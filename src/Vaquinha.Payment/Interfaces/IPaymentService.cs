using System.Threading.Tasks;
using Vaquinha.Domain.Models.Response;
using Vaquinha.Payment.Models;

namespace Vaquinha.Payment
{
    public interface IPaymentService
    {
        Task<PolenUser> AdicionarUsuarioParaLojaAsync(PolenUser user);
        Task<PolenCauseResponse> RecuperarInstituicoesPolenAsync(int page = 0);
        Task<DoacaoDetalheTransacaoResponseModel> AdicionadDoacaoAsync(PolenUserDonation donation);
    }
}
