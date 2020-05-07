using Polly.Retry;

namespace Vaquinha.Domain
{
    public interface IPollyService
    {
        AsyncRetryPolicy CreateAsyncWaitAndRetryPolicyFor(string method);
    }
}
