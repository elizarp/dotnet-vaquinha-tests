using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Interfaces;
using Vaquinha.Domain.Models;

namespace Vaquinha.Service
{
    public class PessoaService : IPessoaService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public PessoaService(IPessoaRepository pessoaRepository,
                             IMapper mapper,
                             IDomainNotificationService domainNotificationService)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task Add(PessoaModel model)
        {
            var entity = _mapper.Map<PessoaModel, Pessoa>(model);

            if (entity.Valido())
            {
                await _pessoaRepository.AddAsync(entity);                
                return;
            }

            _domainNotificationService.Adicionar(entity);
        }

        public async Task Update(PessoaModel model)
        {
            var entity = _mapper.Map<PessoaModel, Pessoa>(model);

            if (entity.Valido())
            {
                await _pessoaRepository.UpdateAsync(entity);
            }

            _domainNotificationService.Adicionar(entity);
        }

        public async Task<IEnumerable<PessoaModel>> GetAllAsync(int pageIndex)
        {
            var pessoas = await _pessoaRepository.GetAllAsync(pageIndex);
            return _mapper.Map<IEnumerable<Pessoa>, IEnumerable<PessoaModel>>(pessoas);
        }
    }
}
