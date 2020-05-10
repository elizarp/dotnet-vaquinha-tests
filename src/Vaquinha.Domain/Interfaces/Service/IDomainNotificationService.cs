using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Vaquinha.Domain.Base;
using Vaquinha.Domain.Entities;

namespace Vaquinha.Domain
{
    public interface IDomainNotificationService
    {
        bool PossuiErros { get; }
        IEnumerable<DomainNotification> RecuperarErrosDominio();
        string RecuperarErrosDominioFormatadoHtml();

        void Adicionar<T>(T entity) where T : Entity;

        void Adicionar(ValidationResult validationResult);
        void Adicionar(DomainNotification domainNotification);
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

        public string RecuperarErrosDominioFormatadoHtml()
        {
            var errors = string.Join("", RecuperarErrosDominio().Select(a => $"<li>{a.MensagemErro}</li>").ToArray());
            return $"<ul>{errors}</ul>";
        }
    }
}