using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Siparis
    {
        public int SiparisID { get; set; }

        [Required(ErrorMessage = "İlan seçimi zorunludur")]
        [Display(Name = "İlan")]
        public int IlanID { get; set; }
        public Ilan Ilan { get; set; }

        [Required(ErrorMessage = "Alıcı seçimi zorunludur")]
        [Display(Name = "Alıcı")]
        public int AliciID { get; set; }
        public Kullanici Alici { get; set; }

        [Required(ErrorMessage = "Adres seçimi zorunludur")]
        [Display(Name = "Teslimat Adresi")]
        public int AdresID { get; set; }
        public Adres Adres { get; set; }

        [Required(ErrorMessage = "Ödeme yöntemi seçimi zorunludur")]
        [Display(Name = "Ödeme Yöntemi")]
        public string OdemeYontemi { get; set; } // "Kapıda Ödeme" veya "Kart ile Ödeme"

        [Display(Name = "Kart")]
        public int? KartID { get; set; }
        public Kart? Kart { get; set; }

        [Required(ErrorMessage = "Sipariş tarihi zorunludur")]
        [Display(Name = "Sipariş Tarihi")]
        public DateTime SiparisTarihi { get; set; }

        [Display(Name = "Sipariş Durumu")]
        public string Durum { get; set; } = "Beklemede"; // "Beklemede", "Onaylandı", "Kargoda", "Teslim Edildi"
    }
}