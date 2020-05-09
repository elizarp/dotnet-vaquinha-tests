using System.Collections.Generic;
using System.Threading.Tasks;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Domain
{
    public interface IPaymentService
    {
        Task<IEnumerable<InstituicaoViewModel>> RecuperarInstituicoesAsync(int page = 0);
        Task AdicionadDoacaoAsync(DoacaoViewModel doacao);
    }
}