using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Vaquinha.Domain;

namespace Vaquinha.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IDomainNotificationService _domainNotificationService;

        public BaseController(IDomainNotificationService domainNotificationService,
                              IToastNotification toastNotification)
        {
            _domainNotificationService = domainNotificationService;
            _toastNotification = toastNotification;
        }

        protected void AdicionarNotificacaoOperacaoRealizadaComSucesso(string mensagemSucesso = null)
        {
            var sucessMessage = mensagemSucesso ?? "Operação realizada com sucesso!";
            _toastNotification.AddSuccessToastMessage(sucessMessage);
        }

        protected void AdicionarErrosDominio()
        {
            var errorMessage = _domainNotificationService.PossuiErros
                ? _domainNotificationService.RecuperarErrosDominioFormatadoHtml()
                : null;

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _toastNotification.AddErrorToastMessage(errorMessage);
            }
        }
    }
}