using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Vaquinha.Domain;
using Vaquinha.Domain.ViewModels;
using Vaquinha.MVC.Models;

namespace Vaquinha.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeInfoService _homeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, 
                              IHomeInfoService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = await _homeService.RecuperarDadosIniciaisHomeAsync();

            // mocado para testes de carousel ( remover aapós ajustes )
            homeViewModel.Doadores = new List<DoadorViewModel> { new DoadorViewModel {Nome = "Doador 1"}, new DoadorViewModel { Nome = "Doador 2" } , new DoadorViewModel { Nome = "Doador 3" } };
            homeViewModel.Instituicoes = new List<InstituicaoViewModel> { new InstituicaoViewModel { Nome = "Instituicao 1" }, new InstituicaoViewModel { Nome = "Instituicao 1" }, new InstituicaoViewModel { Nome = "Instituicao 3" } };

            // adicionar carousel para instituicoes

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
