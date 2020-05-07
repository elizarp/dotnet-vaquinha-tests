using System.Threading.Tasks;
using Vaquinha.Domain.Models;

namespace Vaquinha.Domain
{
    public interface IHomeInfoRepository
    {
        Task<HomeModel> RecuperarDadosIniciaisHomeAsync();
    }
}