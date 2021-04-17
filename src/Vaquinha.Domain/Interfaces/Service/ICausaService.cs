using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Domain
{
    public interface ICausaService
    {
        Task AdicionarAsync(CausaViewModel model);
        Task<IEnumerable<CausaViewModel>> RecuperarCausas();
    }
}