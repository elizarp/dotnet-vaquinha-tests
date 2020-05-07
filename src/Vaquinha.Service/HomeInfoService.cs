using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Models;
using Vaquinha.Payment;
using Vaquinha.Payment.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Vaquinha.Service
{
    public class HomeInfoService : IHomeInfoService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly IHomeInfoRepository _homeRepository;
        private readonly IDoacaoService _doacaoService;
        private readonly IPaymentService _polenService;
        private readonly GloballAppConfig _globalSettings;

        public HomeInfoService(IHomeInfoRepository homeRepository,
                               IMapper mapper,
                               IDoacaoService doacaoService,
                               IPaymentService polenService,
                               GloballAppConfig globalSettings,
                               IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _doacaoService = doacaoService;
            _polenService = polenService;
            _homeRepository = homeRepository;
            _globalSettings = globalSettings;
            _memoryCache = memoryCache;
        }

        public async Task<HomeModel> RecuperarDadosIniciaisHomeAsync()
        {
            HomeModel dadosIniciaisHome = await RecuperarDadosTotaisHome();

            var instituicoes = RecuperarInstituicoesAsync();
            var doacoes = RecuperarDoadoresAsync();

            var diffDate = _globalSettings.DataFimCampanha.Subtract(DateTime.Now);

            dadosIniciaisHome.TempoRestanteDias = diffDate.Days;
            dadosIniciaisHome.TempoRestanteHoras = diffDate.Hours;
            dadosIniciaisHome.TempoRestanteMinutos = diffDate.Minutes;

            dadosIniciaisHome.ValorFaltanteMeta = _globalSettings.MetaCampanha - dadosIniciaisHome.ValorTotalArrecadado;
            dadosIniciaisHome.PorcentagemTotalArrecadado = dadosIniciaisHome.ValorTotalArrecadado * 100 / _globalSettings.MetaCampanha;

            await Task.WhenAll();
            dadosIniciaisHome.Doadores = await doacoes;
            dadosIniciaisHome.Instituicoes = await instituicoes;

            return dadosIniciaisHome;
        }

        private async Task<HomeModel> RecuperarDadosTotaisHome()
        {
            var cacheKey = nameof(_globalSettings.CacheBuscaDadosTotaisEmSegundos);

            if (!_memoryCache.TryGetValue(cacheKey, out HomeModel response))
            {
                response = await _homeRepository.RecuperarDadosIniciaisHomeAsync();
                _memoryCache.Set(cacheKey, response, GetCacheOptions());
            }

            return response;
        }

        public async Task<IEnumerable<InstituicaoModel>> RecuperarInstituicoesAsync(int pageInxex = 0)
        {
            var polenCauses = await _polenService.RecuperarInstituicoesPolenAsync(pageInxex);
            return _mapper.Map<IEnumerable<PolenCauseGetResponse>, IEnumerable<InstituicaoModel>>(polenCauses.Results);
        }

        private async Task<IEnumerable<DoadorModel>> RecuperarDoadoresAsync()
        {
            return await _doacaoService.RecuperarDoadoresAsync();
        }

        private MemoryCacheEntryOptions GetCacheOptions()
        {
            return new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(_globalSettings.CacheBuscaDadosTotaisEmSegundos)
            };
        }
    }
}