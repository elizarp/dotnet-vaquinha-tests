using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Vaquinha.Domain.Models.Input
{
    public class ConvidarPorEmailModel
    {
        public string DestinatariosEmail { get; set; }

        public string[] Emails => DestinatariosEmail?.Replace("\n", string.Empty).Split(",").Distinct().Select(x => x.Trim()).ToArray() ?? new string[] { };

        public bool Valido(ConvidarPorEmailModelValidacao validadorEmail)
        {
            return validadorEmail.Valido(this);
        }
    }

    public class ConvidarPorEmailModelValidacao : AbstractValidator<ConvidarPorEmailModel>
    {
        public ValidationResult ValidationResult { get; set; }

        public bool Valido(ConvidarPorEmailModel emailModel = null)
        {
            emailModel ??= new ConvidarPorEmailModel();

            ValidationResult = Validate(emailModel);
            return ValidationResult.IsValid;
        }

        public ConvidarPorEmailModelValidacao()
        {
            RuleFor(x => x.DestinatariosEmail)
                .NotEmpty().WithMessage("É obrigatório informar ao menos um email.");

            RuleForEach(x => x.Emails)
                .EmailAddress().WithMessage("O email '{PropertyValue}' é inválido.");
        }
    }
}