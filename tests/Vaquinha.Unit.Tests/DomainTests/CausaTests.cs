using FluentAssertions;
using System.Collections.Generic;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;
using System.Linq;

namespace Vaquinha.Unit.Tests.DomainTests
{
	[Collection(nameof(CausaFixtureCollection))]
	public class CausaTests : IClassFixture<CausaFixture>
	{

		#region Atributos
		private readonly CausaFixture _fixture;
		#endregion

		#region Construtores
		public CausaTests(CausaFixture fixture)
		{
			_fixture = fixture;
		}
		#endregion

		#region Métodos/Tests
		[Fact]
		[Trait("Causa", "CausaPreenchidaCorretamente_Valida")]
		public void CausaPreenchidaCorretamente_Valida()
		{
			// Arrange
			var causa = _fixture.CausaValida().FirstOrDefault();

			// Act
			var valido = causa.Valido();

			// Assert
			valido.Should().BeTrue(because: "Todos os campos estão preenchidos conforme o estabelecido.");

			causa.ErrorMessages.Should().BeNullOrEmpty(because: "Pois todos os campos estão preenchidos conforme o estabelecido, não devendo criar ou constar nenhum mensagem de validacão violada.");
		}

		[Fact]
		[Trait("Causa", "CausaPreenchidaCamposNulos_Invalida")]
		public void CausaPreenchidaCamposNulos_Invalida()
		{
			// Arrange
			var causa = _fixture.CausaCamposNulosInvalida().FirstOrDefault();

			// Act
			var valido = causa.Valido();

			// Assert
			valido.Should().BeFalse(because: "todos os campos/propriedades estão com valor padrão/default de seu(s) respectivo(s) tipo.");

			causa.ErrorMessages.Should().HaveCountGreaterOrEqualTo(expected: 1, because: "ao menos um do(s) campo(s) contemplado(s) na validacão da entidade/causa, deveria ter constado.");
		}

		[Fact]
		[Trait("Causa", "CausaCamposEmBranco_Invalida")]
		public void CausaCamposEmBranco_Invalida()
		{
			// Arrange
			var causa = _fixture.CausaVaziaInvalida().FirstOrDefault();

			// Act
			var valido = causa.Valido();

			// Assert
			valido.Should().BeFalse(because: "os campos foram informados com o valor de vazio/empty.");

			causa.ErrorMessages.Should().HaveCountGreaterOrEqualTo(expected: 1, because: "ao menos um do(s) campo(s) contemplado(s) na validacão da entidade/causa, deveria ter constado.");
		}

		[Fact]
		[Trait("Causa", "CamposComQuantidadeCaracteresExcecida_Invalida")]
		public void CamposComQuantidadeCaracteresExcecida_Invalida()
		{
			// Arrange
			var causa = _fixture.CausaMaxLengthCamposExcedidoInvalida().FirstOrDefault();

			// Act
			var valido = causa.Valido();

			// Assert
			valido.Should().BeFalse(because: "todos os campos da entidade/causa foram preenchidos excedendo o tamanho máximo esperado.");

			causa.ErrorMessages.Should().HaveCountGreaterOrEqualTo(expected: 1, because: "ao menos um do(s) campo(s) contemplado(s) na validacão da entidade/causa, deveria ter constado.");
		}

		#endregion
	}
}
