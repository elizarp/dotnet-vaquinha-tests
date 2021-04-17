using AutoMapper;
using Bogus;
using System;
using System.Collections.Generic;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;
using Xunit;
using System.Linq;

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
		public IEnumerable<Causa> CausaValida(int quantidadeEntidades = 1)
		{
			var faker = new Faker<Causa>(_localidade);

			faker
				.RuleFor(c => c.Id, f => Guid.NewGuid())
				.RuleFor(c => c.Nome, f => f.Company.CompanyName())
				.RuleFor(c => c.Cidade, f => f.Address.City())
				.RuleFor(c => c.Estado, f => f.Address.StateAbbr());

			return faker.Generate(quantidadeEntidades);
		}

		public IEnumerable<Causa> CausaVaziaInvalida(int quantidadeEntidades = 1)
		{
			var faker = new Faker<Causa>(_localidade);

			faker
				.RuleFor(c => c.Id, f => Guid.Empty)
				.RuleFor(c => c.Nome, f => string.Empty)
				.RuleFor(c => c.Cidade, f => string.Empty)
				.RuleFor(c => c.Estado, f => string.Empty);

			return faker.Generate(quantidadeEntidades);
		}

		public IEnumerable<Causa> CausaCamposNulosInvalida(int quantidadeEntidades = 1)
		{
			var faker = new Faker<Causa>(_localidade);

			var causa = faker.CustomInstantiator(f => new Causa());

			return causa.Generate(quantidadeEntidades);
		}

		public IEnumerable<Causa> CausaMaxLengthCamposExcedidoInvalida(int quantidadeEntidades = 1)
		{
			var quantidadeCaracteres = (CausaValidacao.MaxLengthCampos + 1);
			var faker = new Faker<Causa>(_localidade);

			faker
				.RuleFor(c => c.Id, f => Guid.NewGuid())
				.RuleFor(c => c.Nome, f => f.Lorem.Letter(quantidadeCaracteres))
				.RuleFor(c => c.Cidade, f => f.Lorem.Letter(quantidadeCaracteres))
				.RuleFor(c => c.Estado, f => f.Lorem.Letter(quantidadeCaracteres));

			return faker.Generate(quantidadeEntidades);
		}

		public IEnumerable<CausaViewModel> CausaViewModelValida(int quantidadeEntidades = 1)
		{
			var causas = CausaValida(quantidadeEntidades);

			var causasViewModel = causas.Select(c => new CausaViewModel { Nome = c.Nome, Cidade = c.Cidade, Estado = c.Estado });

			return causasViewModel;
		}

		public IEnumerable<CausaViewModel> CausaViewModelInvalida(int quantidadeEntidades = 1)
		{
			var causas = CausaVaziaInvalida();
			var causasViewModel = causas.Select(c => new CausaViewModel { Nome = c.Nome, Cidade = c.Cidade, Estado = c.Estado });

			return causasViewModel;

		}

		#endregion
	}
}
