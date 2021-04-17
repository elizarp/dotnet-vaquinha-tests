using FluentValidation;
using System;

using Vaquinha.Domain.Base;


namespace Vaquinha.Domain.Entities
{
	public class Causa : Entity
	{
		public Causa() { }

		public Causa(Guid id, string nome, string cidade, string estado)
		{
			Id = id;
			Nome = nome;
			Cidade = cidade;
			Estado = estado;
		}

		public string Nome { get; private set; }
		public string Cidade { get; private set; }
		public string Estado { get; private set; }

		public override bool Valido()
		{
			ValidationResult = new CausaValidacao().Validate(instance: this);

			return ValidationResult.IsValid;
		}
	}

	public class CausaValidacao : AbstractValidator<Causa>
	{
		#region Atributos
		public static int MaxLengthCampos { get; private set; } = 150;
		#endregion

		#region Construtores
		public CausaValidacao()
		{
			RuleFor(c => c.Id)
				.Cascade(cascadeMode: CascadeMode.StopOnFirstFailure)
				.NotNull().WithMessage("{PropertyName} não pode ser nulo.")
				.NotEmpty().WithMessage("{PropertyName} não pode ser vazio.");

			RuleFor(c => c.Nome)
				.Cascade(cascadeMode: CascadeMode.StopOnFirstFailure)
				.NotNull().WithMessage("{PropertyName} não pode ser nulo.")
				.NotEmpty().WithMessage("{PropertyName} não pode ser vazio.");

			RuleFor(c => c.Cidade)
				.Cascade(cascadeMode: CascadeMode.StopOnFirstFailure)
				.NotNull().WithMessage("{PropertyName} não pode ser nulo.")
				.NotEmpty().WithMessage("{PropertyName} não pode ser vazio.");

			RuleFor(c => c.Estado)
				.Cascade(cascadeMode: CascadeMode.StopOnFirstFailure)
				.NotNull().WithMessage("{PropertyName} não pode ser nulo.")
				.NotEmpty().WithMessage("{PropertyName} não pode ser vazio.");

			RuleFor(c => c.Nome).MaximumLength(MaxLengthCampos);
			RuleFor(c => c.Cidade).MaximumLength(MaxLengthCampos);
			RuleFor(c => c.Estado).MaximumLength(MaxLengthCampos);
		}
		#endregion
	}
}
