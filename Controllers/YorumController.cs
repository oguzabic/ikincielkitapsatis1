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
    public class YorumController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public YorumController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Yorum
        public async Task<IActionResult> Index()
        {
            var yorumlar = await _context.Yorumlar.ToListAsync();
            return View(yorumlar);
        }

        // GET: Yorum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorumlar
                .FirstOrDefaultAsync(m => m.YorumID == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // GET: Yorum/Create
        public IActionResult Create()
        {
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik");
            ViewData["KullaniciID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad");
            return View();
        }

        // POST: Yorum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapID,KullaniciID,YorumMetni,Puan")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                yorum.Tarih = DateTime.Now;
                _context.Add(yorum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik", yorum.KitapID);
            ViewData["KullaniciID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad", yorum.KullaniciID);
            return View(yorum);
        }

        // GET: Yorum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum == null)
            {
                return NotFound();
            }
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik", yorum.KitapID);
            ViewData["KullaniciID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad", yorum.KullaniciID);
            return View(yorum);
        }

        // POST: Yorum/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YorumID,KitapID,KullaniciID,YorumMetni,Puan,Tarih")] Yorum yorum)
        {
            if (id != yorum.YorumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yorum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YorumExists(yorum.YorumID))
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
            ViewData["KitapID"] = new SelectList(_context.Kitaplar, "KitapID", "Baslik", yorum.KitapID);
            ViewData["KullaniciID"] = new SelectList(_context.Kullanicilar, "KullaniciID", "AdSoyad", yorum.KullaniciID);
            return View(yorum);
        }

        // GET: Yorum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorumlar
                .FirstOrDefaultAsync(m => m.YorumID == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // POST: Yorum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum != null)
            {
                _context.Yorumlar.Remove(yorum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YorumExists(int id)
        {
            return _context.Yorumlar.Any(e => e.YorumID == id);
        }
    }
} 