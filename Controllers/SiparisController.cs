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
    public class SiparisController : Controller
    {
        private readonly IkinciElKitapDbContext _context;

        public SiparisController(IkinciElKitapDbContext context)
        {
            _context = context;
        }

        // GET: Siparis
        public async Task<IActionResult> Index()
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var siparisler = await _context.Siparisler
                .Where(s => s.AliciID == kullaniciId)
                .Include(s => s.Ilan)
                .Include(s => s.Alici)
                .ToListAsync();
            return View(siparisler);
        }

        // GET: Siparis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var siparis = await _context.Siparisler
                .Include(s => s.Ilan)
                .Include(s => s.Alici)
                .FirstOrDefaultAsync(m => m.SiparisID == id && m.AliciID == kullaniciId);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // GET: Siparis/Create
        public async Task<IActionResult> Create(int? ilanId)
        {
            if (ilanId == null)
            {
                return RedirectToAction("Index", "Ilan");
            }

            // İlanı kontrol et
            var ilan = await _context.Ilanlar
                .Include(i => i.Kitap)
                .Include(i => i.Satici)
                .FirstOrDefaultAsync(i => i.IlanID == ilanId);

            if (ilan == null)
            {
                return NotFound();
            }

            // Kullanıcının kendi ilanını satın almasını engelle
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (ilan.SaticiID == currentUserId)
            {
                TempData["ErrorMessage"] = "Kendi ilanınızı satın alamazsınız.";
                return RedirectToAction("Index", "Ilan");
            }

            // Kullanıcının adreslerini getir
            var adresler = await _context.Adresler
                .Where(a => a.KullaniciID == currentUserId)
                .ToListAsync();

            // Kullanıcının kartlarını getir
            var kartlar = await _context.Kartlar
                .Where(k => k.KullaniciID == currentUserId)
                .ToListAsync();

            ViewBag.Ilan = ilan;
            ViewBag.Adresler = adresler;
            ViewBag.Kartlar = kartlar;
            return View();
        }

        // POST: Siparis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IlanID,AdresID,OdemeYontemi,KartID,SiparisTarihi")] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                // İlanı kontrol et
                var ilan = await _context.Ilanlar
                    .Include(i => i.Kitap)
                    .FirstOrDefaultAsync(i => i.IlanID == siparis.IlanID);

                if (ilan == null)
                {
                    return NotFound();
                }

                // Kullanıcının kendi ilanını satın almasını engelle
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (ilan.SaticiID == currentUserId)
                {
                    TempData["ErrorMessage"] = "Kendi ilanınızı satın alamazsınız.";
                    return RedirectToAction("Index", "Ilan");
                }

                // Adres kontrolü
                var adres = await _context.Adresler
                    .FirstOrDefaultAsync(a => a.AdresID == siparis.AdresID && a.KullaniciID == currentUserId);
                if (adres == null)
                {
                    ModelState.AddModelError("AdresID", "Geçersiz adres seçimi.");
                    return View(siparis);
                }

                // Kart kontrolü (eğer kart ile ödeme seçildiyse)
                if (siparis.OdemeYontemi == "Kart ile Ödeme")
                {
                    if (!siparis.KartID.HasValue)
                    {
                        ModelState.AddModelError("KartID", "Kart ile ödeme için kart seçimi zorunludur.");
                        return View(siparis);
                    }

                    var kart = await _context.Kartlar
                        .FirstOrDefaultAsync(k => k.KartID == siparis.KartID && k.KullaniciID == currentUserId);
                    if (kart == null)
                    {
                        ModelState.AddModelError("KartID", "Geçersiz kart seçimi.");
                        return View(siparis);
                    }
                }

                siparis.AliciID = currentUserId;
                siparis.SiparisTarihi = DateTime.Now;
                siparis.Durum = "Beklemede";

                // Siparişi ekle
                _context.Add(siparis);

                // İlanı sil
                _context.Ilanlar.Remove(ilan);

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Sipariş başarıyla oluşturuldu!";
                return RedirectToAction(nameof(Index));
            }

            // Hata durumunda ilan bilgilerini tekrar yükle
            var ilanForView = await _context.Ilanlar
                .Include(i => i.Kitap)
                .Include(i => i.Satici)
                .FirstOrDefaultAsync(i => i.IlanID == siparis.IlanID);
            
            var currentUserIdForView = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var adreslerForView = await _context.Adresler
                .Where(a => a.KullaniciID == currentUserIdForView)
                .ToListAsync();
            var kartlarForView = await _context.Kartlar
                .Where(k => k.KullaniciID == currentUserIdForView)
                .ToListAsync();

            ViewBag.Ilan = ilanForView;
            ViewBag.Adresler = adreslerForView;
            ViewBag.Kartlar = kartlarForView;

            return View(siparis);
        }

        // GET: Siparis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var siparis = await _context.Siparisler
                .FirstOrDefaultAsync(m => m.SiparisID == id && m.AliciID == kullaniciId);
            if (siparis == null)
            {
                return NotFound();
            }
            ViewData["IlanID"] = new SelectList(_context.Ilanlar, "IlanID", "Baslik", siparis.IlanID);
            return View(siparis);
        }

        // POST: Siparis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiparisID,IlanID,SiparisTarihi")] Siparis siparis)
        {
            if (id != siparis.SiparisID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    siparis.AliciID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    _context.Update(siparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisExists(siparis.SiparisID))
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
            ViewData["IlanID"] = new SelectList(_context.Ilanlar, "IlanID", "Baslik", siparis.IlanID);
            return View(siparis);
        }

        // GET: Siparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var siparis = await _context.Siparisler
                .Include(s => s.Ilan)
                .Include(s => s.Alici)
                .FirstOrDefaultAsync(m => m.SiparisID == id && m.AliciID == kullaniciId);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var siparis = await _context.Siparisler
                .FirstOrDefaultAsync(m => m.SiparisID == id && m.AliciID == kullaniciId);
            if (siparis != null)
            {
                _context.Siparisler.Remove(siparis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return _context.Siparisler.Any(e => e.SiparisID == id && e.AliciID == kullaniciId);
        }
    }
} 