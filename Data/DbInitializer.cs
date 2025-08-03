using IkinciElKitapProjesi.Models;

namespace IkinciElKitapProjesi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IkinciElKitapDbContext context)
        {
            // Kategorileri ekle
            if (!context.Kategoriler.Any())
            {
                var kategoriler = new Kategori[]
                {
                    new Kategori { KategoriAdi = "Roman" },
                    new Kategori { KategoriAdi = "Bilim-Kurgu" },
                    new Kategori { KategoriAdi = "Tarih" },
                    new Kategori { KategoriAdi = "Felsefe" },
                    new Kategori { KategoriAdi = "Bilim" }
                };

                context.Kategoriler.AddRange(kategoriler);
                context.SaveChanges();
            }
        }
    }
} 