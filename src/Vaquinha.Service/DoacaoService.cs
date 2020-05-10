using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Service
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _polenService;

        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public DoacaoService(IMapper mapper,
                             IDoacaoRepository doacaoRepository,
                             IDomainNotificationService domainNotificationService,
                             IPaymentService polenService)
        {
            _mapper = mapper;
            _polenService = polenService;
            _doacaoRepository = doacaoRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task Adicionar(DoacaoViewModel model)
        {
            //var entity = _mapper.Map<DoacaoViewModel, Doacao>(model);
            //entity.AdicionarDataHora(DateTime.Now);

            //if (!entity.Valido())
            //{
            //    _domainNotificationService.Adicionar(entity);
            //    return;
            //}

            //var detalheTransacao = await RealizarDoacao(entity);

            //if (OperacaoRealizacaComSucesso(detalheTransacao))
            //{
            //    AdicionarDetalheTransacao(entity, detalheTransacao);
            //    await _doacaoRepository.AdicionarAsync(entity);               
            //}

            //_domainNotificationService.Adicionar(detalheTransacao);
        }

        public async Task<IEnumerable<DoadorViewModel>> RecuperarDoadoresAsync(int pageIndex = 0)
        {
            var doadores = await _doacaoRepository.RecuperarDoadoesAsync(pageIndex);
            return _mapper.Map<IEnumerable<Doacao>, IEnumerable<DoadorViewModel>>(doadores);
        }

        public async Task RealizarDoacaoAsync(DoacaoViewModel model)
        {
            //    var userDonation = _mapper.Map<Doacao, UserDonation>(doacao);
            //    return await _polenService.AdicionadDoacaoAsync(userDonation);

            var doacao = _mapper.Map<DoacaoViewModel, Doacao>(model);
            await _doacaoRepository.AdicionarAsync(doacao);
        }
    }
}