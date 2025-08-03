using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Ilan
    {
        public int IlanID { get; set; }

        [Required(ErrorMessage = "Kitap seçimi zorunludur")]
        [Display(Name = "Kitap")]
        public int KitapID { get; set; }
        public Kitap Kitap { get; set; }

        [Required(ErrorMessage = "Satıcı seçimi zorunludur")]
        [Display(Name = "Satıcı")]
        public int SaticiID { get; set; }
        public Kullanici Satici { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0.01, 999999.99, ErrorMessage = "Fiyat 0.01 ile 999999.99 arasında olmalıdır")]
        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }

        [Required(ErrorMessage = "Durum seçimi zorunludur")]
        [Display(Name = "Durum")]
        public string? Durum { get; set; } //İki farklı durumumuz bulunmakta: "Yeni", "İkinci El"

        [StringLength(255, ErrorMessage = "Resim yolu en fazla 255 karakter olabilir")]
        [Display(Name = "Resim Yolu")]
        public string? ResimYolu { get; set; }

        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; }

        [NotMapped]
        public string Baslik => Kitap?.Baslik ?? "İlan #" + IlanID;
    }
}