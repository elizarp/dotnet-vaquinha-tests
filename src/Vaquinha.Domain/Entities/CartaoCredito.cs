using FluentValidation;
using System;
using System.Globalization;
using Vaquinha.Domain.Base;

namespace Vaquinha.Domain.Entities
{
    public class CartaoCredito : Entity
    {
        private CartaoCredito() { }

        public CartaoCredito(string nomeTitular, string numero, string validade, string cvv)
        {
            NomeTitular = nomeTitular;
            NumeroCartaoCredito = numero;
            Validade = validade;
            CVV = cvv;
        }

        public string NomeTitular { get; private set; }
        public string NumeroCartaoCredito { get; private set; }
        public string Validade { get; private set; }
        public string CVV { get; private set; }

        public override bool Valido()
        {
            ValidationResult = new CartaoCreditoValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CartaoCreditoValidacao : AbstractValidator<CartaoCredito>
    {
        private const int MAX_LENTH_CAMPOS = 150;

        public CartaoCreditoValidacao()
        {
            RuleFor(o => o.NomeTitular)
              .NotEmpty().WithMessage("O campo Nome Titular deve ser preenchido")
              .MaximumLength(MAX_LENTH_CAMPOS).WithMessage($"O campo Nome Titular deve possuir no máximo {MAX_LENTH_CAMPOS} caracteres");

            RuleFor(o => o.NumeroCartaoCredito)
                .NotEmpty().WithMessage("O campo Número de cartão de crédito deve ser preenchido")
                .CreditCard().WithMessage("Campo Número de cartão de crédito inválido");

            RuleFor(o => o.CVV)
                .NotEmpty().WithMessage("O campo CVV deve ser preenchido")
                .Must(ValidarCVV)
                .WithMessage("Campo CVV inválido");

            RuleFor(o => o.Validade)
                .NotEmpty().WithMessage("O campo Validade deve ser preenchido")
                .Must(v => ValidarCampoVencimento(v, out _)).WithMessage("Campo Data de Vencimento do cartão de crédito inválido")
                .Must(v => ValidarDataVencimento(v)).WithMessage("Cartão de Crédito com data expirada");
        }

        private bool ValidarCVV(string cvv)
        {
            if (string.IsNullOrEmpty(cvv)) return true;

            return cvv.Length >= 3 && cvv.Length <= 4 && int.TryParse(cvv, out _);
        }

        private bool ValidarCampoVencimento(string validade, out DateTime? data)
        {
            data = null;

            if (string.IsNullOrEmpty(validade)) return true;

            string[] mesAno = validade.Split("/");

            if (mesAno.Length == 2)
            {
                if (mesAno[0].Length <= 2 && mesAno[1].Length <= 4)
                {
                    int mes, ano;
                    if (int.TryParse(mesAno[0], out mes) && int.TryParse(mesAno[1], out ano))
                    {
                        ano = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(ano);
                        if (mes >= 1 && mes <= 12 && ano >= 2000 && ano <= 2099)
                        {
                            data = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool ValidarDataVencimento(string validade)
        {
            if (ValidarCampoVencimento(validade, out DateTime? dataVencimento) && dataVencimento != null)
            {
                return DateTime.Now.Date <= ((DateTime)dataVencimento).Date;
            }

            return true;
        }
    }
}