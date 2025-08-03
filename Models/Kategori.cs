using System.ComponentModel.DataAnnotations;

namespace IkinciElKitapProjesi.Models
{
    public class Kategori
    {
        public int KategoriID { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir")]
        [Display(Name = "Kategori Adı")]
        public string? KategoriAdi { get; set; } //"Bilim-Kurgu", "Hikaye", "Fantezi" vs.

        public virtual ICollection<Kitap>? Kitaplar { get; set; }
    }
}