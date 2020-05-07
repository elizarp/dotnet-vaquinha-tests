using System;

namespace Vaquinha.Domain.Models.Response
{
    public class DoacaoResponseModel
    {
        public Guid Id { get; set; }

        public PessoaModel DadosPessoais { get; set; }
        public DoacaoDetalheTransacaoResponseModel DetalheTransacaoPolen { get; set; }
    }
}