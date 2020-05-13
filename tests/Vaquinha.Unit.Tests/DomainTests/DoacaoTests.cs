using FluentAssertions;
using Xunit;
using Vaquinha.Tests.Common.Fixtures;

namespace Vaquinha.Unit.Tests.DomainTests
{
    [Collection(nameof(DoacaoFixtureCollection))]    
    public class DoacaoTests: IClassFixture<DoacaoFixture>, 
                              IClassFixture<EnderecoFixture>, 
                              IClassFixture<CartaoCreditoFixture>
    {
        private readonly DoacaoFixture _doacaoFixture;
        private readonly EnderecoFixture _enderecoFixture;
        private readonly CartaoCreditoFixture _cartaoCreditoFixture;

        public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }

        [Fact]
        [Trait("Doacao", "Doacao_CorretamentePreenchidos_DoacaoValida")]
        public void Doacao_CorretamentePreenchidos_DoacaoValida()
        {           
            // Arrange
            var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            doacao.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("Doacao", "Doacao_DadosPessoaisInvalidos_DoacaoInvalida")]
        public void Doacao_DadosPessoaisInvalidos_DoacaoInvalida()
        {
            // Arrange
            const bool EMAIL_INVALIDO = true;
            var doacao = _doacaoFixture.DoacaoValida(EMAIL_INVALIDO);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeFalse(because: "o campo email está inválido");
            doacao.ErrorMessages.Should().Contain("O campo Email é inválido.");
            doacao.ErrorMessages.Should().HaveCount(1, because: "somente o campo email está inválido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-5)]
        [InlineData(-10.20)]
        [InlineData(-55.4)]
        [InlineData(-0.1)]
        [Trait("Doacao", "Doacao_ValoresDoacaoMenorIgualZero_DoacaoInvalida")]
        public void Doacao_ValoresDoacaoMenorIgualZero_DoacaoInvalida(double valorDoacao)
        {
            // Arrange            
            var doacao = _doacaoFixture.DoacaoValida(false, valorDoacao);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeFalse(because: "o campo Valor está inválido");
            doacao.ErrorMessages.Should().Contain("Valor mínimo de doação é de R$ 5,00");
            doacao.ErrorMessages.Should().HaveCount(1, because: "somente o campo Valor está inválido.");
        }

        [Theory]
        [InlineData(25000)]
        [InlineData(5500.50)]
        [InlineData(5000.1)]
        [InlineData(4505)]
        [InlineData(4500.1)]
        [Trait("Doacao", "Doacao_ValoresDoacaoMaiorLimite_DoacaoInvalida")]
        public void Doacao_ValoresDoacaoMaiorLimite_DoacaoInvalida(double valorDoacao)
        {
            // Arrange
            const bool EXCEDER_MAX_VALOR_DOACAO = true;
            var doacao = _doacaoFixture.DoacaoValida(false, valorDoacao);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeFalse(because: "o campo Valor está inválido");
            doacao.ErrorMessages.Should().Contain("Valor máximo para a doação é de R$4.500,00");
            doacao.ErrorMessages.Should().HaveCount(1, because: "somente o campo Valor está inválido.");
        }

        [Fact]
        [Trait("Doacao", "Doacao_MensagemApoioMaxlenghtExecido_DoacaoInvalida")]
        public void Doacao_MensagemApoioMaxlenghtExecido_DoacaoInvalida()
        {
            // Arrange
            const bool EXCEDER_MAX_LENTH_MENSAGEM_APOIO = true;
            var doacao = _doacaoFixture.DoacaoValida(false, null, EXCEDER_MAX_LENTH_MENSAGEM_APOIO);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeFalse(because: "O campo Mensagem de Apoio possui mais caracteres do que o permitido");
            doacao.ErrorMessages.Should().HaveCount(1, because: "somente o campo Mensagem deApoio está inválido.");
            doacao.ErrorMessages.Should().Contain("O campo Mensagem de Apoio deve possuir no máximo 500 caracteres.");
        }

        [Fact]
        [Trait("Doacao", "Doacao_DadosNaoInformados_DoacaoInvalida")]
        public void Doacao_DadosNaoInformados_DoacaoInvalida()
        {
            // Arrange
            var doacao = _doacaoFixture.DoacaoInvalida(false);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeFalse(because: "os campos da doação nao foram informados");

            doacao.ErrorMessages.Should().HaveCount(3, because: "Os 3 campos obrigatórios da doação não foram preenchidos");

            doacao.ErrorMessages.Should().Contain("Valor mínimo de doação é de R$ 5,00", because: "valor mínimo de doação nao foi atingido.");
            doacao.ErrorMessages.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            doacao.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");            
        }

        [Fact]
        [Trait("Doacao", "Doacao_DadosNaoInformadosDoacaoAnonima_DoacaoInvalida")]
        public void Doacao_DadosNaoInformadosDoacaoAnonima_DoacaoInvalida()
        {
            // Arrange
            var doacao = _doacaoFixture.DoacaoInvalida(true);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());

            // Act
            var valido = doacao.Valido();

            // Assert
            valido.Should().BeFalse(because: "os campos da doação nao foram informados");

            doacao.ErrorMessages.Should().HaveCount(2, because: "Os 2 campos obrigatórios da doação não foram preenchidos");

            doacao.ErrorMessages.Should().Contain("Valor mínimo de doação é de R$ 5,00", because: "valor mínimo de doação nao foi atingido.");            
            doacao.ErrorMessages.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");            
        }

    }
}
