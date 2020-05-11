using System.ComponentModel;

namespace Vaquinha.Domain.ViewModels
{
    public class PessoaViewModel
    {
        private string _nome { get; set; }
        public string Nome
        {
            get { return Anonima ? "Doação anonima" : _nome; }
            set { _nome = value; }
        }

        public string Email { get; set; }

        [DisplayName("Doação anônima")]
        public bool Anonima { get; set; }

        public string MensagemApoio { get; set; }
    }
}