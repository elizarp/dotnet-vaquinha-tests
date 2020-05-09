using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vaquinha.Domain;

namespace Vaquinha.Service
{
    public class HttpServiceHelper
    {
        private readonly Stopwatch _stopwatch;
        private readonly IPollyService _pollyService;
        private readonly GloballAppConfig _globallAppConfig;

        public HttpServiceHelper(GloballAppConfig globallAppConfig,
                                 IPollyService pollyService)
        {
            _stopwatch = new Stopwatch();
            _globallAppConfig = globallAppConfig;
            _pollyService = pollyService;
        }
        public async Task<T> ExecuteGetRequest<T>(string resource) where T : class
        {
            var client = new RestClient(_globallAppConfig.Correios.BuscaCepBaseUrl);
            var request = new RestRequest(Method.GET);

            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");

            request.Resource = resource;

            var policy = _pollyService.CriarPoliticaWaitAndRetryPara(resource);

            _stopwatch?.Start();
            return await policy.ExecuteAsync(async () => await GetResponseDataAsync<T>(client, request));
        }

        private async Task<T> GetResponseDataAsync<T>(RestClient client, RestRequest request) where T : class
        {
            var response = await client.ExecuteAsync(request);
            _stopwatch?.Stop();

            var content = response.Content;

            var retorno = JsonConvert.DeserializeObject<T>(content);

            return retorno;
        }
    }
}
