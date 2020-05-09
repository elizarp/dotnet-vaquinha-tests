namespace Vaquinha.Domain.ViewModels
{
    public class DoacaoViewModel
    {
        public double Valor { get; private set; }

        public PessoaViewModel DadosPessoais { get; private set; }
        public EnderecoViewModel EnderecoCobranca { get; private set; }
        public CartaoCreditoViewModel FormaPagamento { get; private set; }
    }
}