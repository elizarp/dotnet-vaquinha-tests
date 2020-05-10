using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.MVC.Controllers
{
    public class DoacoesController : BaseController
    {
        private readonly IDoacaoService _doacaoService;
        private readonly IDomainNotificationService _domainNotificationService;

        public DoacoesController(IDoacaoService doacaoService,
                                 IDomainNotificationService domainNotificationService,
                                 IToastNotification toastNotification) : base(domainNotificationService, toastNotification)
        {
            _doacaoService = doacaoService;
            _domainNotificationService = domainNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index), await _doacaoService.RecuperarDoadoresAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoacaoViewModel model)
        {
            _doacaoService.RealizarDoacaoAsync(model);

            if (PossuiErrosDominio())
            {
                AdicionarErrosDominio();
                return View(model);
            }

            AdicionarNotificacaoOperacaoRealizadaComSucesso("Doação realizada com sucesso!<p>Obrigado por apoiar nossa causa :)</p>");
            return RedirectToAction("Index", "Home");
        }

        private bool PossuiErrosDominio()
        {
            return _domainNotificationService.PossuiErros;
        }
    }
}