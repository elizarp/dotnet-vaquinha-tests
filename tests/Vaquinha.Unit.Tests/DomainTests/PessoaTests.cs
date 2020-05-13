using FluentAssertions;
using Xunit;
using Vaquinha.Tests.Common.Fixtures;

namespace Vaquinha.Unit.Tests.DomainTests
{
    [Collection(nameof(PessoaFixtureCollection))]
    public class PessoaTests : IClassFixture<PessoaFixture>
    {
        private readonly PessoaFixture _fixture;

        public PessoaTests(PessoaFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Pessoa", "Pessoa_CorretamentePreenchidos_PessoaValida")]
        public void Pessoa_CorretamentePreenchidos_PessoaValida()
        {
            // Arrange
            var pessoa = _fixture.PessoaValida();
            
            // Act
            var valido = pessoa.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            pessoa.ErrorMessages.Should().BeEmpty();            
        }

        [Fact]
        [Trait("Pessoa", "Pessoa_NenhumDadoPreenchido_PessoaInvalida")]
        public void Pessoa_NenhumDadoPreenchido_PessoaInvalida()
        {
            // Arrange
            var pessoa = _fixture.PessoaVazia();

            // Act
            var valido = pessoa.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");

            pessoa.ErrorMessages.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado.");

            pessoa.ErrorMessages.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            pessoa.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");
        }

        [Fact]
        [Trait("Pessoa", "Pessoa_EmailInvalido_PessoaInvalida")]
        public void Pessoa_EmailInvalido_PessoaInvalida()
        {
            // Arrange
            const bool EMAIL_INVALIDO = true;
            var pessoa = _fixture.PessoaValida(EMAIL_INVALIDO);

            // Act
            var valido = pessoa.Valido();

            // Assert
            valido.Should().BeFalse(because: "o campo email está inválido");
            pessoa.ErrorMessages.Should().HaveCount(1, because: "somente o campo email está inválido.");

            pessoa.ErrorMessages.Should().Contain("O campo Email é inválido.");
        }

        [Fact]
        [Trait("Pessoa", "Pessoa_CamposMaxLenghtExcedidos_PessoaInvalida")]
        public void Pessoa_CamposMaxLenghtExcedidos_PessoaInvalida()
        {
            // Arrange            
            var pessoa = _fixture.PessoaMaxLenth();

            // Act
            var valido = pessoa.Valido();

            // Assert
            valido.Should().BeFalse(because: "os campos nome e email possuem mais caracteres do que o permitido.");
            pessoa.ErrorMessages.Should().HaveCount(2, because: "os dados estão inválidos.");

            pessoa.ErrorMessages.Should().Contain("O campo Nome deve possuir no máximo 150 caracteres.");
            pessoa.ErrorMessages.Should().Contain("O campo Email deve possuir no máximo 150 caracteres.");
        }
    }
}