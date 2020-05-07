using FluentValidation;
using System;
using System.Collections.Generic;
using Vaquinha.Domain.Base;

namespace Vaquinha.Domain.Entities
{
    public class Pessoa : Entity
    {
        public Pessoa() { }

        public Pessoa(Guid id, string nome, string email, bool anonima, string mensagemApoio)
        {
            Id = id;
            _nome = nome;
            Email = email;
            Anonima = anonima;
            MensagemApoio = mensagemApoio;
        }

        private string _nome;

        public string Nome
        {
            get { return Anonima ? Email : _nome; }
            private set { _nome = value; }
        }

        public bool Anonima { get; private set; }
        public string MensagemApoio { get; private set; }

        public string Email { get; private set; }
        public ICollection<Doacao> Doacoes { get; set; }

        public override bool Valido()
        {
            ValidationResult = new PessoaValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PessoaValidacao : AbstractValidator<Pessoa>
    {
        private const int MAX_LENTH_CAMPOS = 150;
        private const int MAX_LENTH_MENSAGEM = 500;

        public PessoaValidacao()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .When(a => a.Anonima == false)
                .MaximumLength(MAX_LENTH_CAMPOS).WithMessage("O campo Nome deve possuir no máximo 150 caracteres.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .MaximumLength(MAX_LENTH_CAMPOS).WithMessage($"O campo Email deve possuir no máximo {MAX_LENTH_CAMPOS} caracteres.");

            RuleFor(a => a.Email).EmailAddress()
                .When(a => !string.IsNullOrEmpty(a.Email))
                .When(a => a?.Email?.Length <= MAX_LENTH_CAMPOS)
                .WithMessage("O campo Email é inválido.");

            RuleFor(a => a.MensagemApoio)                
                .MaximumLength(MAX_LENTH_MENSAGEM).WithMessage($"O campo Mensagem de Apoio deve possuir no máximo {MAX_LENTH_MENSAGEM} caracteres.");
        }
    }
}