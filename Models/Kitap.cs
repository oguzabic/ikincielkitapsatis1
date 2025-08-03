using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkinciElKitapProjesi.Models
{
    public class Kitap
    {
        public int KitapID { get; set; }

        [Required(ErrorMessage = "Kitap başlığı zorunludur")]
        [StringLength(100, ErrorMessage = "Kitap başlığı en fazla 100 karakter olabilir")]
        [Display(Name = "Başlık")]
        public string? Baslik { get; set; }

        [Required(ErrorMessage = "Yazar adı zorunludur")]
        [StringLength(60, ErrorMessage = "Yazar adı en fazla 60 karakter olabilir")]
        [Display(Name = "Yazar")]
        public string? Yazar { get; set; }

        [StringLength(50, ErrorMessage = "Yayınevi adı en fazla 50 karakter olabilir")]
        [Display(Name = "Yayınevi")]
        public string? Yayinevi { get; set; }

        [Range(1800, 2025, ErrorMessage = "Yıl 1800-2025 arasında olmalıdır")]
        [Display(Name = "Yayın Yılı")]
        public int? Yil { get; set; }

        [StringLength(150, ErrorMessage = "Açıklama en fazla 150 karakter olabilir")]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int KategoriID { get; set; }
        public Kategori? Kategori { get; set; }
    }
}