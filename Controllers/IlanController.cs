using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IkinciElKitapProjesi.Data;
using IkinciElKitapProjesi.Models;

namespace IkinciElKitapProjesi.Controllers
{
    public class IlanController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public IlanController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Ilan
        public async Task<IActionResult> Index(int? kategoriId)
        {
            var query = _context.Ilanlar
                .Include(i => i.Kitap)
                .Include(i => i.Satici)
                .AsQueryable();

            // Kategori filtresi
            if (kategoriId.HasValue)
            {
                query = query.Where(i => i.Kitap.KategoriID == kategoriId.Value);
                ViewBag.KategoriId = kategoriId.Value;
            }

            var ilanlar = await query.ToListAsync();
            return View(ilanlar);
        }

        // GET: Ilan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilan = await _context.Ilanlar
                .Include(i => i.Kitap)
                .Include(i => i.Satici)
                .FirstOrDefaultAsync(m => m.IlanID == id);
            if (ilan == null)
            {
                return NotFound();
            }

            return View(ilan);
        }

        // GET: Ilan/Create
        public IActionResult Create()
        {
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik");
            ViewData["SaticiID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad");
            return View();
        }

        // POST: Ilan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapID,SaticiID,Fiyat,Durum")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik", ilan.KitapID);
            ViewData["SaticiID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad", ilan.SaticiID);
            return View(ilan);
        }

        // GET: Ilan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilan = await _context.Ilanlar.FindAsync(id);
            if (ilan == null)
            {
                return NotFound();
            }
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik", ilan.KitapID);
            ViewData["SaticiID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad", ilan.SaticiID);
            return View(ilan);
        }

        // POST: Ilan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IlanID,KitapID,SaticiID,Fiyat,Durum")] Ilan ilan)
        {
            if (id != ilan.IlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlanExists(ilan.IlanID))
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
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik", ilan.KitapID);
            ViewData["SaticiID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad", ilan.SaticiID);
            return View(ilan);
        }

        // GET: Ilan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilan = await _context.Ilanlar
                .Include(i => i.Kitap)
                .Include(i => i.Satici)
                .FirstOrDefaultAsync(m => m.IlanID == id);
            if (ilan == null)
            {
                return NotFound();
            }

            return View(ilan);
        }

        // POST: Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilan = await _context.Ilanlar.FindAsync(id);
            if (ilan != null)
            {
                _context.Ilanlar.Remove(ilan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlanExists(int id)
        {
            return _context.Ilanlar.Any(e => e.IlanID == id);
        }
    }
} 