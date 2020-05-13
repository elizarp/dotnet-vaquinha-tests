using FluentValidation;
using System;
using System.Collections.Generic;
using Vaquinha.Domain.Base;

namespace Vaquinha.Domain.Entities
{
    public class Endereco : Entity
    {
        public Endereco() { }

        public Endereco(Guid id, string cep, string textoEndereco, string complemento, string cidade, string estado, string telefone, string numero)
        {
            Id = id;
            CEP = cep;
            TextoEndereco = textoEndereco;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            Telefone = telefone;
            Numero = numero;
        }
        
        public string CEP { get; private set; }
        public string TextoEndereco { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Telefone { get; private set; }

        public ICollection<Doacao> Doacoes { get; set; }

        public override bool Valido()
        {
            ValidationResult = new EnderecoValidacao().Validate( this);
            return ValidationResult.IsValid;
        }
    }

    public class EnderecoValidacao : AbstractValidator<Endereco>
    {
        private const int MAX_LENGHT_ENDERECO = 250;
        private const int MAX_LENGHT_COMPLEMENTO = 250;
        private const int MAX_LENGHT_CIDADE = 150;
        private const int MAX_LENGHT_NUMERO = 6;

        public EnderecoValidacao()
        {
            RuleFor(o => o.CEP)
                .NotEmpty().WithMessage("O campo CEP deve ser preenchido")
                .Must(ValidarCep).WithMessage("Campo CEP inválido");

            RuleFor(o => o.TextoEndereco)
                .NotEmpty().WithMessage("O campo Endereço deve ser preenchido")
                .MaximumLength(MAX_LENGHT_ENDERECO).WithMessage($"O campo Endereço deve possuir no máximo {MAX_LENGHT_ENDERECO} caracteres");

            RuleFor(o => o.Numero)
                .NotEmpty().WithMessage("O campo Número deve ser preenchido")
                .MaximumLength(MAX_LENGHT_NUMERO).WithMessage($"O campo Número deve possuir no máximo {MAX_LENGHT_NUMERO} caracteres");

            RuleFor(o => o.Cidade)
                .NotEmpty().WithMessage("O campo Cidade deve ser preenchido")
                .MaximumLength(MAX_LENGHT_CIDADE).WithMessage($"O campo Cidade deve possuir no máximo {MAX_LENGHT_CIDADE} caracteres");

            RuleFor(o => o.Estado)                
                .Length(2).WithMessage("Campo Estado inválido");

            RuleFor(o => o.Telefone)
                .NotEmpty().WithMessage("O campo Telefone deve ser preenchido")
                .Must(ValidarTelefone).WithMessage("Campo Telefone inválido");

            RuleFor(o => o.Complemento)
                .MaximumLength(MAX_LENGHT_COMPLEMENTO).WithMessage($"O campo Complemento deve possuir no máximo {MAX_LENGHT_COMPLEMENTO} caracteres");
        }

        private bool ValidarCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return true;

            return cep.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Length == 8;
        }

        private bool ValidarTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone)) return true;

            var tamanho = telefone.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty)
                .Replace("-", string.Empty).Length;

            return tamanho >= 10 && tamanho <= 11;
        }
    }
}