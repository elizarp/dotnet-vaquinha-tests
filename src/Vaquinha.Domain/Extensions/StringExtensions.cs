using System.Linq;

namespace Vaquinha.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ToPolenApiToken(this string requestUri, GloballAppConfig globallAppConfig)
        {
            return $"{requestUri}?api_token={globallAppConfig.Polen.ApiToken}";
        }

        public static string ToPolenCausesQueryStringParameters(this string resource, int page, GloballAppConfig globallAppConfig)
        {
            var queryStringParameters = resource.ToPolenApiToken(globallAppConfig);
            return queryStringParameters + $"&storeId={globallAppConfig.Polen.StoreId}&userId=&page=&pageSize=";
        }

        public static string OnlyNumbers(this string text)
        {
            return string.Concat(text?.Where(char.IsDigit))?.Trim();
        }
    }
}
