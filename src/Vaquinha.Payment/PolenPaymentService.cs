using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Extensions;
using Vaquinha.Domain.ViewModels;
using Vaquinha.Payment.PolenEntities.Output;

namespace Vaquinha.Payment
{
    public class PolenPaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly GloballAppConfig _globallAppConfig;
        private readonly IPolenHttpServiceHelper _httpServiceHelper;

        public PolenPaymentService(GloballAppConfig globallAppConfig,
                                   IPolenHttpServiceHelper httpServiceHelper,
                                   IMapper mapper)
        {
            _globallAppConfig = globallAppConfig;
            _httpServiceHelper = httpServiceHelper;
            _mapper = mapper;
        }

        public async Task AdicionadDoacaoAsync(DoacaoViewModel doacao)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CausaViewModel>> RecuperarInstituicoesAsync(int page = 0)
        {
            var resource = "api/v1/cause".ToPolenCausesQueryStringParameters(page, _globallAppConfig);
            var response = await _httpServiceHelper.ExecuteGetRequestAsync<CauseResponse>(resource);

            if (response == null || response.Results == null) return null;

            response.Results = response.Results.Where(a => a.Active);

            return _mapper.Map<CauseResponse, IEnumerable<CausaViewModel>>(response);
        }
    }
}
