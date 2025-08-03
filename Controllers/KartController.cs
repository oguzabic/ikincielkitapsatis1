using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IkinciElKitapProjesi.Data;
using IkinciElKitapProjesi.Models;

namespace IkinciElKitapProjesi.Controllers
{
    [Authorize]
    public class KartController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public KartController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Kart
        public async Task<IActionResult> Index()
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var kartlar = await _context.Kartlar
                .Where(k => k.KullaniciID == kullaniciId)
                .ToListAsync();
            return View(kartlar);
        }

        // GET: Kart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var kart = await _context.Kartlar
                .FirstOrDefaultAsync(m => m.KartID == id && m.KullaniciID == kullaniciId);
            if (kart == null)
            {
                return NotFound();
            }

            return View(kart);
        }

        // GET: Kart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KartNumarasi,SonKullanmaTarihi,CVV,KartTipi")] Kart kart)
        {
            if (ModelState.IsValid)
            {
                // Kart numarasındaki boşlukları temizle
                if (!string.IsNullOrEmpty(kart.KartNumarasi))
                {
                    kart.KartNumarasi = kart.KartNumarasi.Replace(" ", "");
                }
                
                kart.KullaniciID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _context.Add(kart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kart);
        }

        // GET: Kart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var kart = await _context.Kartlar
                .FirstOrDefaultAsync(m => m.KartID == id && m.KullaniciID == kullaniciId);
            if (kart == null)
            {
                return NotFound();
            }
            return View(kart);
        }

        // POST: Kart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KartID,KartNumarasi,SonKullanmaTarihi,CVV,KartTipi")] Kart kart)
        {
            if (id != kart.KartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    kart.KullaniciID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    _context.Update(kart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartExists(kart.KartID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kart);
        }

        // GET: Kart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var kart = await _context.Kartlar
                .FirstOrDefaultAsync(m => m.KartID == id && m.KullaniciID == kullaniciId);
            if (kart == null)
            {
                return NotFound();
            }

            return View(kart);
        }

        // POST: Kart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var kart = await _context.Kartlar
                .FirstOrDefaultAsync(m => m.KartID == id && m.KullaniciID == kullaniciId);
            if (kart != null)
            {
                _context.Kartlar.Remove(kart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartExists(int id)
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _context.Kartlar.Any(e => e.KartID == id && e.KullaniciID == kullaniciId);
        }
    }
} 