using System.Threading.Tasks;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Domain
{
    public interface IHomeInfoRepository
    {
        Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync();
    }
}