using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vaquinha.Domain;
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
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
