using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Models;

namespace Vaquinha.Domain
{
    public interface IPessoaService
    {
        Task Add(PessoaModel model);
        Task Update(PessoaModel model);
        Task<IEnumerable<PessoaModel>> GetAllAsync(int pageIndex);
    }
}
