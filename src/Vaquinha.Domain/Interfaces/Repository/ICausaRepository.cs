using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Domain
{
    public interface ICausaRepository
    {
        Task<Causa> Adicionar(Causa causa);
        Task<IEnumerable<Causa>> RecuperarCausas();
    }
}
