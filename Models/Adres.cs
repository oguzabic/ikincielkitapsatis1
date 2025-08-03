using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Adres
    {
        public int AdresID { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        [StringLength(50, ErrorMessage = "Başlık en fazla 50 karakter olabilir")]
        [Display(Name = "Adres Başlığı")]
        public string? Baslik { get; set; }

        [Required(ErrorMessage = "İl seçimi zorunludur")]
        [Display(Name = "İl")]
        public string? Il { get; set; }

        [Required(ErrorMessage = "İlçe seçimi zorunludur")]
        [Display(Name = "İlçe")]
        public string? Ilce { get; set; }

        [Required(ErrorMessage = "Adres detayı zorunludur")]
        [StringLength(200, ErrorMessage = "Adres detayı en fazla 200 karakter olabilir")]
        [Display(Name = "Adres Detayı")]
        public string? AdresDetay { get; set; }

        [StringLength(10, ErrorMessage = "Posta kodu en fazla 10 karakter olabilir")]
        [Display(Name = "Posta Kodu")]
        public string? PostaKodu { get; set; }

        [ForeignKey("KullaniciID")]
        public int KullaniciID { get; set; }
        public Kullanici? Kullanici { get; set; }
    }
}