using Microsoft.EntityFrameworkCore;
using IkinciElKitapProjesi.Models;

namespace IkinciElKitapProjesi.Data
{
    public class IkinciElKitapDbContext : DbContext
    {
        public IkinciElKitapDbContext(DbContextOptions<IkinciElKitapDbContext> options) : base(options) { }

        public DbSet<Adres> Adresler { get; set; }
        public DbSet<Kart> Kartlar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Ilan> Ilanlar { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Navigation property'leri tanımlama
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Kullanici)
                .WithMany()
                .HasForeignKey(a => a.KullaniciID);

            modelBuilder.Entity<Kart>()
                .HasOne(k => k.Kullanici)
                .WithMany()
                .HasForeignKey(k => k.KullaniciID);

            modelBuilder.Entity<Kitap>()
                .HasOne(k => k.Kategori)
                .WithMany()
                .HasForeignKey(k => k.KategoriID);

            modelBuilder.Entity<Ilan>()
                .HasOne(i => i.Kitap)
                .WithMany()
                .HasForeignKey(i => i.KitapID);

            modelBuilder.Entity<Ilan>()
                .HasOne(i => i.Satici)
                .WithMany()
                .HasForeignKey(i => i.SaticiID);

            modelBuilder.Entity<Siparis>()
                .HasOne(s => s.Ilan)
                .WithMany()
                .HasForeignKey(s => s.IlanID);

            modelBuilder.Entity<Siparis>()
                .HasOne(s => s.Alici)
                .WithMany()
                .HasForeignKey(s => s.AliciID);

            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.Kitap)
                .WithMany()
                .HasForeignKey(y => y.KitapID);

            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.Kullanici)
                .WithMany()
                .HasForeignKey(y => y.KullaniciID);
        }
    }
}
