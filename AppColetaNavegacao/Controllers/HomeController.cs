using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppColetaNavegacao.Models;

namespace AppColetaNavegacao.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!MetodosGenericos.UsuarioAtivo(User))
            {
                return RedirectToAction("Login", "Identity/Account");
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
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
