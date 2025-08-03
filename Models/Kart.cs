using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Kart
    {
        public int KartID { get; set; }

        [Display(Name = "Kullanıcı")]
        public int KullaniciID { get; set; }
        public Kullanici Kullanici { get; set; }

        [Required(ErrorMessage = "Kart numarası zorunludur")]
        [Display(Name = "Kart Numarası")]
        public string? KartNumarasi { get; set; }

        [Required(ErrorMessage = "Son kullanma tarihi zorunludur")]
        [Display(Name = "Son Kullanma Tarihi")]
        [DataType(DataType.Date)]
        public DateTime SonKullanmaTarihi { get; set; }

        [Required(ErrorMessage = "CVV zorunludur")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV 3 karakter olmalıdır")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV 3 haneli rakam olmalıdır")]
        [Display(Name = "CVV")]
        public string? CVV { get; set; }

        [Required(ErrorMessage = "Kart tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Kart tipi en fazla 50 karakter olabilir")]
        [Display(Name = "Kart Tipi")]
        public string? KartTipi { get; set; } //"Kredi" veya "Banka"
    }
}