using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Service
{
    public class HomeInfoService : IHomeInfoService
    {
        private readonly IMapper _mapper;
        private readonly IDoacaoService _doacaoService;
        private readonly IPaymentService _polenService;
        private readonly GloballAppConfig _globalSettings;
        private readonly IHomeInfoRepository _homeRepository;

        public HomeInfoService(IMapper mapper,
                               IDoacaoService doacaoService,
                               IPaymentService polenService,
                               GloballAppConfig globalSettings,
                               IHomeInfoRepository homeRepository)
        {
            _mapper = mapper;
            _doacaoService = doacaoService;
            _polenService = polenService;
            _homeRepository = homeRepository;
            _globalSettings = globalSettings;
        }

        public async Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync()
        {
            var dadosIniciaisHome = await RecuperarDadosTotaisHome();

            var instituicoes = RecuperarInstituicoesAsync();
            var doacoes = RecuperarDoadoresAsync();

            var diffDate = _globalSettings.DataFimCampanha.Subtract(DateTime.Now);

            dadosIniciaisHome.TempoRestanteDias = diffDate.Days;
            dadosIniciaisHome.TempoRestanteHoras = diffDate.Hours;
            dadosIniciaisHome.TempoRestanteMinutos = diffDate.Minutes;

            dadosIniciaisHome.ValorRestanteMeta = _globalSettings.MetaCampanha - dadosIniciaisHome.ValorTotalArrecadado;
            dadosIniciaisHome.PorcentagemTotalArrecadado = dadosIniciaisHome.ValorTotalArrecadado * 100 / _globalSettings.MetaCampanha;

            await Task.WhenAll();
            dadosIniciaisHome.Doadores = await doacoes;
            dadosIniciaisHome.Instituicoes = await instituicoes;

            return dadosIniciaisHome;
        }

        private async Task<HomeViewModel> RecuperarDadosTotaisHome()
        {
            return await _homeRepository.RecuperarDadosIniciaisHomeAsync();
        }

        public async Task<IEnumerable<InstituicaoViewModel>> RecuperarInstituicoesAsync(int pageInxex = 0)
        {
            return await _polenService.RecuperarInstituicoesAsync(pageInxex);
        }

        private async Task<IEnumerable<DoadorViewModel>> RecuperarDoadoresAsync()
        {
            return await _doacaoService.RecuperarDoadoresAsync();
        }
    }
}