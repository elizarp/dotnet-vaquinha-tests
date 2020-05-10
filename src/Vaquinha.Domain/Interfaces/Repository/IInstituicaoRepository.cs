using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Domain
{
    public interface IInstituicaoRepository
    {
        Task<IEnumerable<Instituicao>> RecuperarInstituicoes();
    }
}
