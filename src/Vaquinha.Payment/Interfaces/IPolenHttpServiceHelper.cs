using System.Threading.Tasks;
using Vaquinha.Domain;

namespace Vaquinha.Payment
{
    public interface IPolenHttpServiceHelper
    {
        Task<T> ExecuteGetRequestAsync<T>(string resource) where T : IConvideDezenoveDiagnostics;
        Task<TOut> ExecutePostRequest<TIn, TOut>(string resource, TIn body) where TIn : class where TOut : IConvideDezenoveDiagnostics;
    }
}
