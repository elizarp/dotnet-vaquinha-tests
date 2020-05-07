using System;

namespace Vaquinha.Domain.Models
{
    public class DoacaoModel
    {
        public DoacaoModel()
        {            

            DadosPessoais = new PessoaModel();
            EnderecoCobranca = new EnderecoModel();
            FormaPagamento = new CartaoCreditoModel();            
        }

        public decimal Valor { get; set; }

        public DateTime DataHora { get; set; }
        public PessoaModel DadosPessoais { get; set; }
        public CartaoCreditoModel FormaPagamento { get; set; }
        public EnderecoModel EnderecoCobranca { get; set; }
    }
}