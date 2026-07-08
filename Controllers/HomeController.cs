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
        List<FiguritaUsuario> nuevas = new List<FiguritaUsuario>();
        foreach (Jugadores j in sobre)
        {
            FiguritaUsuario f = new FiguritaUsuario();
            f.idJugador = j.ID;
            nuevas.Add(f);
        }
        BD.AbrirSobre(nuevas);
        ViewBag.Sobre = sobre;
        return View();
    }
    
     public IActionResult Pegar(int id)
    {
        List<FiguritaUsuario> figuritas = BD.ObtenerFiguritas();
        FiguritaUsuario pegar = null;
        foreach(FiguritaUsuario f in figuritas)
        {
            if(f.idJugador == id)
            {
                pegar = f;
            }
        }
        if(pegar != null)
        {
            BD.PegarFigurita(pegar);
        }
        return RedirectToAction("Album");
    }

    public IActionResult Album()
    {
        ViewBag.Album = BD.ObtenerFiguritasPegadas();
        ViewBag.Jugadores = BD.ObtenerJugadores();
        return View();
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
