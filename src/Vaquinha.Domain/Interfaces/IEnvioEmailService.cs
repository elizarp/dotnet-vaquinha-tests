using System.Threading.Tasks;
using Vaquinha.Domain.Models.Input;

namespace Vaquinha.Domain
{
    public interface IEnvioEmailService
    {
        bool EmailValido(ConvidarPorEmailModel destinatariosEmail);
        Task EnviarEmailAsync(ConvidarPorEmailModel destinatariosEmail);
    }
}