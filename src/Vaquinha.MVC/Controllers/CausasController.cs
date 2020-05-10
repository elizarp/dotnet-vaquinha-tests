using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.MVC.Controllers
{
    public class CausasController : Controller
    {
        private readonly ICausaService _causaService;

        public CausasController(ICausaService causaService)
        {
            _causaService = causaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _causaService.RecuperarCausas());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CausaViewModel model)
        {
            await _causaService.Adicionar(model);
            return RedirectToAction("Index");
        }
    }
}