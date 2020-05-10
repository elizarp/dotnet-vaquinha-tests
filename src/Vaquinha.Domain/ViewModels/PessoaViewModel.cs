using System.ComponentModel;

namespace Vaquinha.Domain.ViewModels
{
    public class PessoaViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        [DisplayName("Doação anônima")]
        public bool Anonima { get; set; }

        [DisplayName("Mensagem de Apoio")]
        public string MensagemApoio { get; set; }
    }
}