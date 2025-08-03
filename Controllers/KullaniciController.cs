using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IkinciElKitapProjesi.Data;
using IkinciElKitapProjesi.Models;

namespace IkinciElKitapProjesi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public KullaniciController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Kullanicilar.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null) return NotFound();
            return View(kullanici);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Soyad,Email,Sifre,Rol")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                // Email kontrolü - aynı email ile kayıt var mı?
                var existingUser = await _context.Kullanicilar.FirstOrDefaultAsync(k => k.Email == kullanici.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu email adresi zaten kullanılıyor.");
                    return View(kullanici);
                }

                _context.Add(kullanici);
                await _context.SaveChangesAsync();
                
                // Kayıt başarılı mesajı
                TempData["SuccessMessage"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                return RedirectToAction("Index", "Login");
            }
            return View(kullanici);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null) return NotFound();
            return View(kullanici);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("KullaniciID,Ad,Soyad,Email,Sifre,Rol")] Kullanici kullanici)
        {
            if (id != kullanici.KullaniciID) return NotFound();
            _context.Update(kullanici);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null) return NotFound();
            return View(kullanici);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 