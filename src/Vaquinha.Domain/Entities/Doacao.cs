using FluentValidation;
using System;
using Vaquinha.Domain.Base;

namespace Vaquinha.Domain.Entities
{
    public class Doacao : Entity
    {
        private Doacao() { }

        public Doacao(Guid id, Guid dadosPessoaisId, Guid enderecoCobrancaId, Guid detalheTransacaoId,
            double valor, Pessoa dadosPessoais, CartaoCredito formaPagamento, Endereco enderecoCobranca)
        {
            Id = id;
            DadosPessoaisId = dadosPessoaisId;
            EnderecoCobrancaId = enderecoCobrancaId;
            DetalheTransacaoId = detalheTransacaoId;
            Valor = valor;
            DataHora = DateTime.Now;
            DadosPessoais = dadosPessoais;
            EnderecoCobranca = enderecoCobranca;
            FormaPagamento = formaPagamento;
        }

        public double Valor { get; private set; }

        public Guid DadosPessoaisId { get; private set; }
        public Guid EnderecoCobrancaId { get; private set; }
        public Guid DetalheTransacaoId { get; private set; }

        public DateTime DataHora { get; private set; }
        public Pessoa DadosPessoais { get; private set; }
        public Endereco EnderecoCobranca { get; private set; }
        public CartaoCredito FormaPagamento { get; private set; }
        public DoacaoDetalheTransacao DetalheTransacao { get; private set; }

        public void AdicionarDataHora(DateTime dataHora)
        {
            DataHora = dataHora;
        }

        public void AdicionarFormaPagamento(CartaoCredito cartaoCredito)
        {
            FormaPagamento = cartaoCredito;
        }
        public void AdicionarEnderecoCobranca(Endereco endereco)
        {
            EnderecoCobranca = endereco;
        }
        public void AdicionarPessoa(Pessoa pessoa)
        {
            DadosPessoais = pessoa;
        }

        public override bool Valido()
        {
            ValidationResult = new DoacaoValidacao().Validate(this);
            return ValidationResult.IsValid;
        }

        public void AdiconarDetalheTransacaoDoacao(DoacaoDetalheTransacao detalheTransacao)
        {
            DetalheTransacao = detalheTransacao;
        }
    }

    public class DoacaoValidacao : AbstractValidator<Doacao>
    {
        public DoacaoValidacao()
        {
            RuleFor(a => a.Valor)
                .GreaterThanOrEqualTo(5).WithMessage("Valor mínimo de doação é de R$ 5,00")
                .LessThanOrEqualTo(4500).WithMessage("Valor máximo para a doação é de R$4.500,00");
            RuleFor(a => a.DadosPessoais).NotNull().WithMessage("Os Dados Pessoais são obrigatórios").SetValidator(new PessoaValidacao());
            RuleFor(a => a.EnderecoCobranca).NotNull().WithMessage("O Endereço de Cobrança é obtigatório.").SetValidator(new EnderecoValidacao());
            RuleFor(a => a.FormaPagamento).NotNull().WithMessage("A Forma de Pagamento é obtigatória.").SetValidator(new CartaoCreditoValidacao());
        }
    }
}