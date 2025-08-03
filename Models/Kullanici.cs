using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Kullanici
    {
        public int KullaniciID { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(40, ErrorMessage = "Ad en fazla 40 karakter olabilir")]
        [Display(Name = "Ad")]
        public string? Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [StringLength(40, ErrorMessage = "Soyad en fazla 40 karakter olabilir")]
        [Display(Name = "Soyad")]
        public string? Soyad { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Email en fazla 100 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Şifre en fazla 100 karakter olabilir")]
        [Display(Name = "Şifre")]
        public string? Sifre { get; set; }

        [Required(ErrorMessage = "Rol alanı zorunludur")]
        [Display(Name = "Rol")]
        public string? Rol { get; set; } //Sadece üç rolümüz var "Admin", "Satıcı", "Alıcı"

        [NotMapped]
        public string AdSoyad => $"{Ad} {Soyad}";

        public virtual ICollection<Ilan>? Ilanlar { get; set; }
        public virtual ICollection<Siparis>? Siparisler { get; set; }
        public virtual ICollection<Yorum>? Yorumlar { get; set; }
        public virtual ICollection<Adres>? Adresler { get; set; }
        public virtual ICollection<Kart>? Kartlar { get; set; }
    }
}