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
using Vaquinha.Domain.Extensions;

namespace Vaquinha.Service
{
    public class CorreiosHttpServiceHelper : ICorreiosHttpServiceHelper
    {
        private readonly Stopwatch _stopwatch;
        private readonly IPollyService _pollyService;
        private readonly GloballAppConfig _globallAppConfig;

        public CorreiosHttpServiceHelper(GloballAppConfig globallAppConfig,
                                 IPollyService pollyService)
        {
            _stopwatch = new Stopwatch();
            _globallAppConfig = globallAppConfig;
            _pollyService = pollyService;
        }
        public async Task<T> ExecuteGetRequest<T>(string resource) where T : IConvideDezenoveDiagnostics
        {
            var client = new RestClient(_globallAppConfig.Correios.BuscaCepBaseUrl);
            var request = new RestRequest(Method.GET);

            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");

            request.Resource = resource;

            var policy = _pollyService.CreateAsyncWaitAndRetryPolicyFor(resource);

            _stopwatch?.Start();
            return await policy.ExecuteAsync(async () => await GetResponseDataAsync<T>(client, request));
        }

        private async Task<T> GetResponseDataAsync<T>(RestClient client, RestRequest request) where T : IConvideDezenoveDiagnostics
        {
            var response = await client.ExecuteAsync(request);
            _stopwatch?.Stop();

            response.EnsureSuccessStatusCode();

            var content = response.Content;

            var retorno = JsonConvert.DeserializeObject<T>(content);

            retorno.ElapsedMilliseconds = _stopwatch?.ElapsedMilliseconds ?? 0;

            return retorno;
        }

    }
}
