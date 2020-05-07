namespace Vaquinha.Domain.Models
{
    public class EnderecoModel
    {
        public string CEP { get; set; }
        public string TextoEndereco { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }

        public string Numero { get; set; }
    }

    public class EnderecoResponse : IConvideDezenoveDiagnostics
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}