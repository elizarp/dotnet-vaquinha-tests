using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.Extensions;

namespace Vaquinha.Payment
{
    public class PolenHttpServiceHelper : IPolenHttpServiceHelper
    {   
        private readonly GloballAppConfig _globallAppConfig;

        public PolenHttpServiceHelper(GloballAppConfig globallAppConfig)
        {
            _globallAppConfig = globallAppConfig;
        }

        public async Task<TOut> ExecutePostRequest<TIn, TOut>(string resource, TIn body) where TIn : class
        {
            var client = new RestClient(_globallAppConfig.Polen.BaseUrlV2);

            var request = CreatePostRequest(resource);

            AddJsonBody(body, request);


            return await GetResponseDataAsync<TOut>(client, request);
        }

        public async Task<T> ExecuteGetRequestAsync<T>(string resource) where T : class
        {
            var client = new RestClient(_globallAppConfig.Polen.BaseUrlV1);
            var request = new RestRequest(Method.GET);

            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");

            request.Resource = resource;

            var response = await GetResponseDataAsync<T>(client, request);

            return response;
        }

        private RestRequest CreatePostRequest(string resource)
        {
            var request = new RestRequest()
            {
                Method = Method.POST
            };

            request.Parameters.Clear();

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.Resource = resource.ToPolenApiToken(_globallAppConfig);

            return request;
        }

        private static void AddJsonBody<T>(T body, RestRequest request) where T : class
        {
            var contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };

            var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            request.AddJsonBody(json);
        }

        private async Task<T> GetResponseDataAsync<T>(RestClient client, RestRequest request)
        {
            var response = await client.ExecuteAsync(request);

            var content = response.Content;

            var retorno = JsonConvert.DeserializeObject<T>(content);

            return retorno;
        }
    }
}
