using System.Threading.Tasks;
using Vaquinha.Domain;

namespace Vaquinha.Service
{
    public interface ICorreiosHttpServiceHelper
    {

        Task<T> ExecuteGetRequest<T>(string resource) where T : IConvideDezenoveDiagnostics;

    }
}
