using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Models;

namespace Vaquinha.Domain
{
    public interface IHomeInfoService
    {
        Task<HomeModel> RecuperarDadosIniciaisHomeAsync();        
        Task<IEnumerable<InstituicaoModel>> RecuperarInstituicoesAsync(int pageInxex = 0);
    }
}
