using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IkinciElKitapProjesi.Data;
using IkinciElKitapProjesi.Models;
using System.Diagnostics;

namespace IkinciElKitapProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IkinciElKitapDbContext _context;

        public HomeController(ILogger<HomeController> logger, IkinciElKitapDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Son eklenen 6 ilanı getir
            var sonEklenenIlanlar = await _context.Ilanlar
                .Include(i => i.Kitap)
                .Include(i => i.Satici)
                .OrderByDescending(i => i.IlanID)
                .Take(6)
                .ToListAsync();

            ViewBag.SonEklenenIlanlar = sonEklenenIlanlar;
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
