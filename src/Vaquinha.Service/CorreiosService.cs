using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Models;

namespace Vaquinha.Service
{
    public class CorreiosService : ICorreiosService
    {
        private readonly IMapper _mapper;
        private readonly GloballAppConfig _globallAppConfig;
        private readonly ILogger<CorreiosService> _logger;
        private readonly ICorreiosHttpServiceHelper _httpServiceHelper;
        private readonly IDomainNotificationService _domainNotificationService;

        public CorreiosService(GloballAppConfig globallAppConfig, 
                               IMapper mapper,
                               ICorreiosHttpServiceHelper httpServiceHelper,
                               IDomainNotificationService domainNotificationService, 
                               ILogger<CorreiosService> correiosService)
        {
            _globallAppConfig = globallAppConfig;
            _httpServiceHelper = httpServiceHelper;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
            _logger = correiosService;
        }

        public async Task<EnderecoModel> RecuperarEnderecoAsync(string cep)
        {
            if (!ValidarCep(cep))
            {
                _domainNotificationService.Adicionar(new DomainNotification("cep", "CEP de busca inválido"));
                return null;
            }

            var resource = $"{cep}/json";
            _logger.LogInformation("Inicio integracao com correios");

            var endereco = await _httpServiceHelper.ExecuteGetRequest<EnderecoResponse>(resource);

            if (string.IsNullOrEmpty(endereco.cep))
            {
                _domainNotificationService.Adicionar(new DomainNotification("cep", "CEP não encontrado"));
                return null;
            }

            return _mapper.Map<EnderecoResponse, EnderecoModel>(endereco);
        }

        private bool ValidarCep(string cep)
        {
            Regex Rgx = new Regex(@"^\d{8}$");
            return Rgx.IsMatch(cep.Replace("-", string.Empty).Replace(" ", string.Empty));
        }
    }
}
