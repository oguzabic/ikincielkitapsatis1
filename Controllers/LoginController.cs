using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IkinciElKitapProjesi.Data;
using IkinciElKitapProjesi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IkinciElKitapProjesi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public LoginController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email, string sifre)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                ModelState.AddModelError("", "Email ve şifre gereklidir");
                return View();
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == email && k.Sifre == sifre);

            if (kullanici == null)
            {
                ModelState.AddModelError("", "Geçersiz email veya şifre");
                return View();
            }

            // Kullanıcıyı giriş yap
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciID.ToString()),
                new Claim(ClaimTypes.Name, kullanici.Ad + " " + kullanici.Soyad),
                new Claim(ClaimTypes.Email, kullanici.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        // GET: Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
} 