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
using IkinciElKitapProjesi.Helpers;

namespace IkinciElKitapProjesi.Controllers
{
    [Authorize]
    public class AdresController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public AdresController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Adres
        public async Task<IActionResult> Index()
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adresler = await _context.Adresler
                .Where(a => a.KullaniciID == kullaniciId)
                .ToListAsync();
            return View(adresler);
        }

        // GET: Adres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adres = await _context.Adresler
                .FirstOrDefaultAsync(m => m.AdresID == id && m.KullaniciID == kullaniciId);
            if (adres == null)
            {
                return NotFound();
            }

            return View(adres);
        }

        // GET: Adres/Create
        public IActionResult Create()
        {
            ViewBag.Iller = new SelectList(TurkiyeYerlesimBirimleri.GetIller());
            return View();
        }

        // POST: Adres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Baslik,Il,Ilce,AdresDetay,PostaKodu")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                adres.KullaniciID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _context.Add(adres);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Iller = new SelectList(TurkiyeYerlesimBirimleri.GetIller());
            return View(adres);
        }

        // GET: Adres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adres = await _context.Adresler
                .FirstOrDefaultAsync(m => m.AdresID == id && m.KullaniciID == kullaniciId);
            if (adres == null)
            {
                return NotFound();
            }
            ViewBag.Iller = new SelectList(TurkiyeYerlesimBirimleri.GetIller());
            return View(adres);
        }

        // POST: Adres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdresID,Baslik,Il,Ilce,AdresDetay,PostaKodu")] Adres adres)
        {
            if (id != adres.AdresID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    adres.KullaniciID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    _context.Update(adres);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresExists(adres.AdresID))
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
            ViewBag.Iller = new SelectList(TurkiyeYerlesimBirimleri.GetIller());
            return View(adres);
        }

        // GET: Adres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adres = await _context.Adresler
                .FirstOrDefaultAsync(m => m.AdresID == id && m.KullaniciID == kullaniciId);
            if (adres == null)
            {
                return NotFound();
            }

            return View(adres);
        }

        // POST: Adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adres = await _context.Adresler
                .FirstOrDefaultAsync(m => m.AdresID == id && m.KullaniciID == kullaniciId);
            if (adres != null)
            {
                _context.Adresler.Remove(adres);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // AJAX: İlçeleri getir
        [HttpGet]
        public JsonResult GetIlceler(string il)
        {
            var ilceler = TurkiyeYerlesimBirimleri.GetIlceler(il);
            return Json(ilceler);
        }

        private bool AdresExists(int id)
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _context.Adresler.Any(e => e.AdresID == id && e.KullaniciID == kullaniciId);
        }
    }
} 