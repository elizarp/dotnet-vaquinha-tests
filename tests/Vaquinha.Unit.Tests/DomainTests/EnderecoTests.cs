using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Vaquinha.Tests.Common.Fixtures;
namespace Vaquinha.Unit.Tests.DomainTests
{
    [Collection(nameof(EnderecoFixtureCollection))]
    public class EnderecoTests: IClassFixture<EnderecoFixture>
    {
        private readonly EnderecoFixture _fixture;

        public EnderecoTests(EnderecoFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Endereco", "Endereco_CorretamentePreenchido_EnderecoValido")]
        public void Endereco_CorretamentePreenchido_EnderecoValido()
        {
            // Arrange
            var endereco = _fixture.EnderecoValido();

            // Act
            var valido = endereco.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            endereco.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Endereco", "Endereco_NenhumDadoPreenchido_EnderecoInvalido")]
        public void Endereco_NenhumDadoPreenchido_EnderecoInvalido()
        {
            // Arrange
            var endereco = _fixture.EnderecoVazio();

            // Act
            var valido = endereco.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de preenchimento");
            endereco.ErrorMessages.Should().HaveCount(6, because: "nenhum dos 6 campos obrigatórios foi informado ou estão incorretos.");

            endereco.ErrorMessages.Should().Contain("O campo Cidade deve ser preenchido", because: "o campo Cidade é obrigatório e não foi preenchido.");
            endereco.ErrorMessages.Should().Contain("O campo Endereço deve ser preenchido", because: "o campo Endereço é obrigatório e não foi preenchido.");
            endereco.ErrorMessages.Should().Contain("O campo CEP deve ser preenchido", because: "o campo CEP é obrigatório e não foi preenchido.");
            endereco.ErrorMessages.Should().Contain("Campo Estado inválido", because: "o campo Estado não foi preenchido c omnforme o esperado.");
            endereco.ErrorMessages.Should().Contain("O campo Telefone deve ser preenchido", because: "o campo Telefone não foi preenchido comnforme o esperado.");
            endereco.ErrorMessages.Should().Contain("O campo Número deve ser preenchido", because: "o campo Número não foi preenchido comnforme o esperado.");
        }

        [Fact]
        [Trait("Endereco", "Endereco_CepTelefoneEstadoInvalido_EnderecoInvalido")]
        public void Endereco_CepTelefoneEstadoInvalido_EnderecoInvalido()
        {
            // Arrange
            var endereco = _fixture.EnderecoCepTelefoneEstadoInvalido();

            // Act
            var valido = endereco.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            endereco.ErrorMessages.Should().HaveCount(3, because: "o preenchimento de 3 campos não foi feito conforme o esperado.");
            
            endereco.ErrorMessages.Should().Contain("Campo CEP inválido", because: "o campo CEP não foi preenchido comnforme o esperado.");
            endereco.ErrorMessages.Should().Contain("Campo Estado inválido", because: "o campo Estado não foi preenchido comnforme o esperado.");
            endereco.ErrorMessages.Should().Contain("Campo Telefone inválido", because: "o campo Telefone não foi preenchido comnforme o esperado.");            
        }

        [Fact]
        [Trait("Endereco", "Endereco_EnderecoCidadeComplementoMaxLength_EnderecoInvalido")]
        public void Endereco_EnderecoCidadeComplementoMaxLength_EnderecoInvalido()
        {
            // Arrange
            var endereco = _fixture.EnderecoMaxLength();

            // Act
            var valido = endereco.Valido();

            // Assert
            valido.Should().BeFalse(because: "tamanho máximo de campos atingidos");
            endereco.ErrorMessages.Should().HaveCount(4, because: "o preenchimento de 4 campos ultrapassou tamanho máximo permitido.");

            endereco.ErrorMessages.Should().Contain("O campo Endereço deve possuir no máximo 250 caracteres", because: "o campo Endereço ultrapassou tamanho máximo permitido.");
            endereco.ErrorMessages.Should().Contain("O campo Cidade deve possuir no máximo 150 caracteres", because: "o campo Cidade ultrapassou tamanho máximo permitido.");
            endereco.ErrorMessages.Should().Contain("O campo Complemento deve possuir no máximo 250 caracteres", because: "o campo Complemento ultrapassou tamanho máximo permitido.");
            endereco.ErrorMessages.Should().Contain("O campo Número deve possuir no máximo 6 caracteres", because: "o campo Complemento ultrapassou tamanho máximo permitido.");
        }
        

    }
}
