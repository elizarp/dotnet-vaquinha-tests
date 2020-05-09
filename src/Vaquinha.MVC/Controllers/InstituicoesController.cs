using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vaquinha.MVC.Controllers
{
    public class InstituicoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}