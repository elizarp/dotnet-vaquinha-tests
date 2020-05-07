using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Domain.Interfaces
{
    public interface IDoacaoRepository
    {
        Task<Doacao> AdicionarAsync(Doacao model);
        Task<IEnumerable<Doacao>> RecuperarDoadoesAsync(int pageIndex = 0);
    }
}