using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP04.Models;

namespace TP04.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

        public IActionResult Index()
    {
        return View();
    }

   public IActionResult AbrirSobre()
    {
        List<Jugadores> sobre = BD.GenerarSobre();
        BD.AbrirSobre(sobre);
        return View(sobre);
    }

    public IActionResult Album()
    {
        ViewBag.Jugadores = BD.ObtenerJugadores();
        return View(BD.ObtenerFiguritas());
    }

    public IActionResult Pegar(int id)
    {
        BD.PegarFigurita(id);
        return RedirectToAction("Album");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
