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
    public class KitapController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public KitapController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Kitap
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kitaplar.ToListAsync());
        }

        // GET: Kitap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // GET: Kitap/Create
        public IActionResult Create()
        {
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: Kitap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Baslik,Yazar,Yayinevi,Yil,Aciklama,KategoriID")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriAdi", kitap.KategoriID);
            return View(kitap);
        }

        // GET: Kitap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriAdi", kitap.KategoriID);
            return View(kitap);
        }

        // POST: Kitap/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapID,Baslik,Yazar,Yayinevi,Yil,Aciklama,KategoriID")] Kitap kitap)
        {
            if (id != kitap.KitapID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.KitapID))
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
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriAdi", kitap.KategoriID);
            return View(kitap);
        }

        // GET: Kitap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // POST: Kitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap != null)
            {
                _context.Kitaplar.Remove(kitap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
            return _context.Kitaplar.Any(e => e.KitapID == id);
        }

        // AJAX: Kitap oluştur
        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] Kitap kitap)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(kitap);
                    await _context.SaveChangesAsync();
                    
                    return Json(new { 
                        success = true, 
                        kitapID = kitap.KitapID, 
                        baslik = kitap.Baslik 
                    });
                }
                else
                {
                    return Json(new { 
                        success = false, 
                        message = "Geçersiz veri" 
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { 
                    success = false, 
                    message = ex.Message 
                });
            }
        }
    }
} 