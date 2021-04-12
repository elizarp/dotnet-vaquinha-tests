using Bogus;
using System;
using Vaquinha.Domain.Entities;
using Xunit;

namespace Vaquinha.Tests.Common.Fixtures
{
	[CollectionDefinition(nameof(CausaFixtureCollection))]
	public class CausaFixtureCollection : ICollectionFixture<CausaFixture>
	{ }

	public class CausaFixture
	{
		#region Atributos
		private readonly string _localidade = "pt_BR";
		#endregion

		#region Métodos
		public Causa CausaValida()
		{
			var faker = new Faker<Causa>(_localidade);

			faker
				.RuleFor(c => c.Id, f => Guid.NewGuid())
				.RuleFor(c => c.Nome, f => f.Company.CompanyName())
				.RuleFor(c => c.Cidade, f => f.Address.City())
				.RuleFor(c => c.Estado, f => f.Address.StateAbbr());

			return faker.Generate();
		}

		public Causa CausaVaziaInvalida()
		{
			var faker = new Faker<Causa>(_localidade);

			faker
				.RuleFor(c => c.Id, f => Guid.Empty)
				.RuleFor(c => c.Nome, f => string.Empty)
				.RuleFor(c => c.Cidade, f => string.Empty)
				.RuleFor(c => c.Estado, f => string.Empty);

			return faker.Generate();
		}

		public Causa CausaCamposNulosInvalida()
		{
			var faker = new Faker<Causa>(_localidade);

			var causa = faker.CustomInstantiator(f => new Causa());

			return causa;
		}

		public Causa CausaMaxLengthCamposExcedidoInvalida()
		{
			var quantidadeCaracteres = (CausaValidacao.MaxLengthCampos + 1);
			var faker = new Faker<Causa>(_localidade);

			faker
				.RuleFor(c => c.Id, f => Guid.NewGuid())
				.RuleFor(c => c.Nome, f => f.Lorem.Letter(quantidadeCaracteres))
				.RuleFor(c => c.Cidade, f => f.Lorem.Letter(quantidadeCaracteres))
				.RuleFor(c => c.Estado, f => f.Lorem.Letter(quantidadeCaracteres));

			return faker.Generate();
		}
		#endregion
	}
}
