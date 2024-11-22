using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using semeando_mvc.Models;

namespace semeando_mvc.Controllers;

/// <summary>
/// Controller responsável por gerenciar páginas gerais do sistema.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Exibe a página inicial (Splash Screen).
    /// </summary>
    /// <returns>Retorna a view da página inicial.</returns>
    public IActionResult Index()
    {
        return View();
    }
    

    /// <summary>
    /// Página de política de privacidade.
    /// </summary>
    /// <returns>Retorna a view da política de privacidade.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Página de erro para capturar exceções.
    /// </summary>
    /// <returns>Retorna a view de erro.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
