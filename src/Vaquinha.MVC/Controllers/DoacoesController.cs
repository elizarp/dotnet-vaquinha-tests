using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.MVC.Controllers
{
    public class DoacoesController : Controller
    {
        private readonly IDoacaoService _doacaoService;

        public DoacoesController(IDoacaoService doacaoService)
        {
            _doacaoService = doacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _doacaoService.RecuperarDoadoresAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DoacaoViewModel model)
        {
            await _doacaoService.RealizarDoacaoAsync(model);
            return RedirectToAction("Index");
        }
    }
}