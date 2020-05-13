using Polly.Retry;

namespace Vaquinha.Service
{
    public interface IPollyService
    {
        AsyncRetryPolicy CriarPoliticaWaitAndRetryPara(string method);
    }
}
