namespace Vaquinha.Domain.Models
{
    public class PessoaModel
    {
        public bool Anonima { get; set; }
    
        public string Nome { get; set; }
        public string Email { get; set; }

        public string MensagemApoio { get; set; }
    }
}