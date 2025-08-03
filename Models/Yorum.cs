using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Yorum
    {
        public int YorumID { get; set; }

        [Required(ErrorMessage = "Kitap seçimi zorunludur")]
        [Display(Name = "Kitap")]
        public int KitapID { get; set; }
        public Kitap Kitap { get; set; }

        [Required(ErrorMessage = "Kullanıcı seçimi zorunludur")]
        [Display(Name = "Kullanıcı")]
        public int KullaniciID { get; set; }
        public Kullanici Kullanici { get; set; }

        [Required(ErrorMessage = "Yorum metni zorunludur")]
        [StringLength(500, ErrorMessage = "Yorum metni en fazla 500 karakter olabilir")]
        [Display(Name = "Yorum")]
        public string? YorumMetni { get; set; }

        [Range(1, 5, ErrorMessage = "Puan 1-5 arasında olmalıdır")]
        [Display(Name = "Puan")]
        public int? Puan { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; }
    }
}