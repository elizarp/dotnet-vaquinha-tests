using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Service
{
	public class CausaService : ICausaService
	{
		private readonly IMapper _mapper;
		private readonly ICausaRepository _causaRepository;
		private readonly IDomainNotificationService _domainNotificationService;

		public CausaService(IMapper mapper,
							 ICausaRepository causaRepository,
							 IDomainNotificationService domainNotificationService)
		{
			_mapper = mapper;
			_causaRepository = causaRepository;
			_domainNotificationService = domainNotificationService;
		}

		public async Task AdicionarAsync(CausaViewModel model)
		{
			var causa = _mapper.Map<CausaViewModel, Causa>(model);

			if (causa.Valido())
			{
				await _causaRepository.AdicionarAsync(causa);
				return;
			}

			_domainNotificationService.Adicionar(causa);
		}

		public async Task<IEnumerable<CausaViewModel>> RecuperarCausas()
		{
			var causas = await _causaRepository.RecuperarCausas();
			return _mapper.Map<IEnumerable<Causa>, IEnumerable<CausaViewModel>>(causas);
		}
	}
}