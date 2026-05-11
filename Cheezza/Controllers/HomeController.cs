using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cheezza.Models;
using Cheezza.Data; // Veritaban? ba?lant?s? için eklendi
using Microsoft.EntityFrameworkCore; // ToListAsync için eklendi

namespace Cheezza.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context; // Veritaban? havuzumuz

    // Dependency Injection ile veritaban?n? içeri al?yoruz
    public HomeController(ILogger<HomeController> _logger, ApplicationDbContext context)
    {
        this._logger = _logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Veritaban?ndaki tüm pizzalar? listele ve sayfaya gönder
        var pizzalar = await _context.Pizzas.ToListAsync();
        return View(pizzalar);
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