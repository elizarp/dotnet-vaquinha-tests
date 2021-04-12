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
				.NotNull()
				.NotEmpty();

			RuleFor(c => c.Nome)
				.NotNull()
				.NotEmpty();

			RuleFor(c => c.Cidade)
				.NotNull()
				.NotEmpty();

			RuleFor(c => c.Estado)
				.NotNull()
				.NotEmpty();

			RuleFor(c => c.Nome).MaximumLength(MaxLengthCampos);
			RuleFor(c => c.Cidade).MaximumLength(MaxLengthCampos);
			RuleFor(c => c.Estado).MaximumLength(MaxLengthCampos);
		}
		#endregion
	}
}
