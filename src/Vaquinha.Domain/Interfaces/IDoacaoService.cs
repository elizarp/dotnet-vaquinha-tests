using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Models;
using Vaquinha.Domain.Models.Response;

namespace Vaquinha.Domain
{
    public interface IDoacaoService
    {
        Task<DoacaoResponseModel> Adicionar(DoacaoModel model);
        Task<IEnumerable<DoadorModel>> RecuperarDoadoresAsync(int pageIndex = 0);
    }
}