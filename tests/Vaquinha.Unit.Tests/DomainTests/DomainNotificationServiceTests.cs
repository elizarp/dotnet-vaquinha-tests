using FluentAssertions;
using Xunit;
using Vaquinha.Domain;
using Vaquinha.Domain.Entities;
using System.Linq;
using Vaquinha.Tests.Common.Fixtures;

namespace Vaquinha.Unit.Tests.DomainTests
{
    [Collection(nameof(PessoaFixtureCollection))]
    public class DomainNotificationServiceTests: IClassFixture<PessoaFixture>
    {
        private readonly PessoaFixture _pessoaFixture;
        private readonly IDomainNotificationService _domainNotificationService;

        public DomainNotificationServiceTests(PessoaFixture fixture)
        {
            _pessoaFixture = fixture;
            _domainNotificationService = new DomainNotificationService();
        }

        [Trait("DomainNotificationService", "DomainNotificationService_NovaClasse_NaoDevePossuirNotificacoes")]
        [Fact]
        public void DomainNotificationService_NovaClasse_NaoDevePossuirNotificacoes()
        {
            // Arrange & Act
            var domainNotification = new DomainNotificationService();

            // Assert
            domainNotification.PossuiErros.Should().BeFalse(because:"ainda não foi adicionado nenhuma notificadao de dominino");
        }
        
        [Trait("DomainNotificationService", "DomainNotificationService_AdicionarNotificacao_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AdicionarNotificacao_HasNotificationsTrue()
        {
            // Arrange
            var domainNotification = new DomainNotification("RequiredField", "O campo Nome é obrigatório");

            // Act
            _domainNotificationService.Adicionar(domainNotification);

            // Assert            
            _domainNotificationService.PossuiErros.Should().BeTrue(because: "foi adicionado a notificacao de codigo RequiredField");

            var notifications = _domainNotificationService.RecuperarErrosDominio().Select(a => a.MensagemErro);
            notifications.Should().Contain("O campo Nome é obrigatório", because: "foi adicionado a notificacao de codigo RequiredField");
        }

        [Trait("DomainNotificationService", "DomainNotificationService_AdicionarEntidade_HasNotificationsTrue")]
        [Fact]
        public void DomainNotificationService_AdicionarEntidade_HasNotificationsTrue()
        {
            // Arrange
            var pessoa = _pessoaFixture.PessoaVazia();
            pessoa.Valido();

            // Act
            _domainNotificationService.Adicionar(pessoa);

            // Assert
            var notifications = _domainNotificationService.RecuperarErrosDominio().Select(a => a.MensagemErro);

            notifications.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado.");
            notifications.Should().Contain("O campo Nome é obrigatório.", because: "o campo Nome não foi informado.");
            notifications.Should().Contain("O campo Email é obrigatório.", because: "o campo Email não foi informado.");

            _domainNotificationService.PossuiErros.Should().BeTrue(because: "foi adicionado a entidade pessoa inválida");
        }
    }
}