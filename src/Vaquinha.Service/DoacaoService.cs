using AutoMapper;
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
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public DoacaoService(IMapper mapper,
                             IDoacaoRepository doacaoRepository,
                             IDomainNotificationService domainNotificationService)
        {
            _mapper = mapper;
            _doacaoRepository = doacaoRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task RealizarDoacaoAsync(DoacaoViewModel model)
        {
            var entity = _mapper.Map<DoacaoViewModel, Doacao>(model);

            entity.AtualizarDataCompra();

            if (entity.Valido())
            {
                await _doacaoRepository.AdicionarAsync(entity);
                return;
            }

            _domainNotificationService.Adicionar(entity);
        }

        public async Task<IEnumerable<DoadorViewModel>> RecuperarDoadoresAsync(int pageIndex = 0)
        {
            var doadores = await _doacaoRepository.RecuperarDoadoesAsync(pageIndex);
            return _mapper.Map<IEnumerable<Doacao>, IEnumerable<DoadorViewModel>>(doadores);
        }
    }
}