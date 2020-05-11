namespace Vaquinha.Domain.ViewModels
{
    public class DoacaoViewModel
    {
        public decimal Valor { get; set; }

        public PessoaViewModel DadosPessoais { get; set; }
        public EnderecoViewModel EnderecoCobranca { get; set; }
        public CartaoCreditoViewModel FormaPagamento { get; set; }
    }
}