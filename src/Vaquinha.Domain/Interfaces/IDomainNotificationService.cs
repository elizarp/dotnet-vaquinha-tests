using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Vaquinha.Domain.Base;
using Vaquinha.Domain.Configuration;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.Models.Response;

namespace Vaquinha.Domain
{
    public interface IDomainNotificationService
    {
        bool PossuiErros { get; }
        IEnumerable<DomainNotification> RecuperarErrosDominio();

        void Adicionar<T>(T entity) where T : Entity;

        void Adicionar(ValidationResult validationResult);
        void Adicionar(DomainNotification domainNotification);
        void Adicionar(DoacaoDetalheTransacaoResponseModel responseTransacaoPagamentoDoacao);
    }

    public class DomainNotificationService : IDomainNotificationService
    {
        private readonly List<DomainNotification> _notifications;

        public bool PossuiErros => _notifications.Any();

        public DomainNotificationService()
        {
            _notifications = new List<DomainNotification>();
        }

        public IEnumerable<DomainNotification> RecuperarErrosDominio()
        {
            return _notifications;
        }

        public void Adicionar(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }

        public void Adicionar<T>(T entity) where T : Entity
        {
            var notifications = entity.ValidationResult.Errors.Select(a => new DomainNotification(a.ErrorCode, a.ErrorMessage));
            _notifications.AddRange(notifications);
        }

        public void Adicionar(ValidationResult validationResult)
        {
            if (validationResult == null) return;

            var notifications = validationResult.Errors.Select(a => new DomainNotification(a.ErrorCode, a.ErrorMessage));
            _notifications.AddRange(notifications);
        }

        public void Adicionar(DoacaoDetalheTransacaoResponseModel responseTransacaoPagamentoDoacao)
        {
            responseTransacaoPagamentoDoacao ??= new DoacaoDetalheTransacaoResponseModel();

            AdicionarErrosTransacaoPolen(responseTransacaoPagamentoDoacao);
            AdicionarErrosCartaoCredito(responseTransacaoPagamentoDoacao);
        }

        private void AdicionarErrosTransacaoPolen(DoacaoDetalheTransacaoResponseModel responseTransacaoPagamentoDoacao)
        {
            var polenStatusErrorCode = RecuperarCodigoStatusPolen(responseTransacaoPagamentoDoacao);

            var notification = CriarNotificacaoDominioParaErrosPolen(polenStatusErrorCode, responseTransacaoPagamentoDoacao.Description);

            AdicionarErroDominio(notification);
        }

        private void AdicionarErrosCartaoCredito(DoacaoDetalheTransacaoResponseModel responseTransacaoPagamentoDoacao)
        {
            var cartaoCreditoInvalido = responseTransacaoPagamentoDoacao.ErrorCode == (int)PolenResponseStatus.CreditCardInvalid;

            if (cartaoCreditoInvalido)
            {
                var creditCardErros = responseTransacaoPagamentoDoacao?.Creditcard_errors;

                if (creditCardErros != null)
                {
                    AdicionarErroCartaoCredito(PolenResponseStatus.CreditCardFirstNameInvalid, creditCardErros?.CardNumber);
                    AdicionarErroCartaoCredito(PolenResponseStatus.CreditCardLastNameInvalid, creditCardErros?.CardNumber);
                    AdicionarErroCartaoCredito(PolenResponseStatus.CreditCardCardNumberInvalid, creditCardErros?.CardNumber);
                    AdicionarErroCartaoCredito(PolenResponseStatus.CreditCardYearInvalid, creditCardErros?.CardNumber);
                    AdicionarErroCartaoCredito(PolenResponseStatus.CreditCardMonthCodeInvalid, creditCardErros?.CardNumber);
                    AdicionarErroCartaoCredito(PolenResponseStatus.CreditCardSecurityCodeInvalid, creditCardErros?.CardNumber);
                }
            }
        }

        private void AdicionarErroCartaoCredito(PolenResponseStatus codigoErroPolen, string mensagemDefaultPolen)
        {
            if (ExisteErroParaPropriedade(mensagemDefaultPolen))
            {
                var notification = CriarNotificacaoDominioParaErrosPolen(codigoErroPolen, mensagemDefaultPolen);
                AdicionarErroDominio(notification);
            }
        }

        private static DomainNotification CriarNotificacaoDominioParaErrosPolen(PolenResponseStatus polenStatusErrorCode, string mensagemDefaultPolen)
        {
            var mensagemDePara = DePolenErrorMessageParaConvideDezenoveDomain.RecuperarMensagemDominio(polenStatusErrorCode, mensagemDefaultPolen);

            return new DomainNotification(polenStatusErrorCode.ToString(), mensagemDePara);
        }

        private static PolenResponseStatus RecuperarCodigoStatusPolen(DoacaoDetalheTransacaoResponseModel responseTransacaoPagamentoDoacao)
        {
            var polenErrorCode = responseTransacaoPagamentoDoacao?.ErrorCode ?? 0;
            return (PolenResponseStatus)polenErrorCode;
        }

        private static bool ExisteErroParaPropriedade(string campo)
        {
            return !string.IsNullOrEmpty(campo);
        }

        private void AdicionarErroDominio(DomainNotification notification)
        {
            if (CampoPreenchido(notification) && !ErroExiste(notification.MensagemErro))
            {
                _notifications.Add(notification);
            }
        }

        private bool ErroExiste(string campo)
        {
            return _notifications.Any(a => a.MensagemErro == campo);
        }

        private static bool CampoPreenchido(DomainNotification notification)
        {
            return !string.IsNullOrEmpty(notification.MensagemErro);
        }
    }
}