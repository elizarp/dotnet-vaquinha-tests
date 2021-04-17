using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.MVC.Controllers
{
	public class CausaController : BaseController
	{
		#region Atributos
		private readonly ICausaService _service;
		#endregion

		#region Construtores
		public CausaController(
			ICausaService service,
			IDomainNotificationService notifications,
			IToastNotification toast)
			: base(notifications, toast)
		{
			_service = service;
		}
		#endregion

		#region Métodos / Acões
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			try
			{
				var causas = await _service.RecuperarCausas();

				return View(model: causas);
			}
			catch
			{
				throw;
			}
		}

		[HttpGet]
		public IActionResult Create()
		{
			try
			{
				return View();
			}
			catch
			{
				throw;
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(CausaViewModel model)
		{
			try
			{
				await _service.AdicionarAsync(model);

				if (PossuiErrosDominio())
				{
					AdicionarErrosDominio();
					return View(model);
				}

				AdicionarNotificacaoOperacaoRealizadaComSucesso("Causa cadastrada com sucesso!");
				return RedirectToAction("Index");
			}
			catch
			{
				throw;
			}
		}

		#endregion
	}
}
