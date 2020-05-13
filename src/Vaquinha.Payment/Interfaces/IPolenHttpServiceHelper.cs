using System.Threading.Tasks;

namespace Vaquinha.Payment
{
    public interface IPolenHttpServiceHelper
    {
        Task<T> ExecuteGetRequestAsync<T>(string resource) where T : class;
        Task<TOut> ExecutePostRequest<TIn, TOut>(string resource, TIn body) where TIn : class;
    }
}