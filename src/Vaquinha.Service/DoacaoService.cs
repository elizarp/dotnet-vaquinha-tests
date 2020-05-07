using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Interfaces;
using Vaquinha.Domain.Models;
using Vaquinha.Domain.Models.Response;
using Vaquinha.Payment;
using Vaquinha.Payment.Models;

namespace Vaquinha.Service
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly GloballAppConfig _globallAppConfig;

        private readonly IMapper _mapper;
        private readonly IPaymentService _polenService;
        
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public DoacaoService(IMapper mapper,
                             IDoacaoRepository doacaoRepository,
                             IDomainNotificationService domainNotificationService,
                             IPaymentService polenService,
                             IMemoryCache memoryCache, 
                             GloballAppConfig globallAppConfig)
        {
            _mapper = mapper;
            _polenService = polenService;
            _doacaoRepository = doacaoRepository;
            _domainNotificationService = domainNotificationService;
            _memoryCache = memoryCache;
            _globallAppConfig = globallAppConfig;
        }

        public async Task<DoacaoResponseModel> Adicionar(DoacaoModel model)
        {
            var entity = _mapper.Map<DoacaoModel, Doacao>(model);
            entity.AdicionarDataHora(DateTime.Now);

            if (!entity.Valido())
            {
                _domainNotificationService.Adicionar(entity);
                return null;
            }

            var detalheTransacao = await RealizarDoacao(entity);

            if (OperacaoRealizacaComSucesso(detalheTransacao))
            {
                AdicionarDetalheTransacao(entity, detalheTransacao);
                await _doacaoRepository.AdicionarAsync(entity);

                return new DoacaoResponseModel
                {
                    Id = entity.Id,
                    DadosPessoais = model.DadosPessoais,
                    DetalheTransacaoPolen = detalheTransacao
                };
            }

            _domainNotificationService.Adicionar(detalheTransacao);
            return null;
        }

        private void AdicionarDetalheTransacao(Doacao doacao, DoacaoDetalheTransacaoResponseModel model)
        {
            var detalheTransacao = _mapper.Map<DoacaoDetalheTransacaoResponseModel, DoacaoDetalheTransacao>(model);
            doacao.AdiconarDetalheTransacaoDoacao(detalheTransacao);
        }

        public async Task<IEnumerable<DoadorModel>> RecuperarDoadoresAsync(int pageIndex = 0)
        {
            var cacheKey = nameof(_globallAppConfig.CacheBuscaDoacoesEmSegundos);

            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<DoadorModel> response))
            {
                var doadores = await _doacaoRepository.RecuperarDoadoesAsync(pageIndex);
                response = _mapper.Map<IEnumerable<Doacao>, IEnumerable<DoadorModel>>(doadores);

                _memoryCache.Set(cacheKey, response, GetCacheOptions());
            }

            return response;
        }

        private async Task<DoacaoDetalheTransacaoResponseModel> RealizarDoacao(Doacao doacao)
        {
            var userDonation = _mapper.Map<Doacao, PolenUserDonation>(doacao);
            return await _polenService.AdicionadDoacaoAsync(userDonation);
        }

        private bool OperacaoRealizacaComSucesso(DoacaoDetalheTransacaoResponseModel detalheTransacao)
        {
            return Convert.ToBoolean(detalheTransacao?.Success ?? "false");
        }

        private MemoryCacheEntryOptions GetCacheOptions()
        {
            return new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(_globallAppConfig.CacheBuscaDoacoesEmSegundos)
            };
        }
    }
}