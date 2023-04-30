using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YemekTarifleri.Models
{
    [Table("Tbl_Tarifler")]
	public class Recip
	{
        [Key]
        [Column("tarifId")]
        public int? id { get; set; }
        [Column("tarifAd")]
        public string? name { get; set; }
        [Column("tarifMalzeme")]
        public string? ingredients { get; set; }
        [Column("tarifYapilis")]
        public string? cooking { get; set; }
        [Column("tarifResim")]
        public string? image { get; set; }
        [Column("tarifTarih")]
        public string? date { get; set; }
        [Column("tarifPuan")]
        public double? point { get; set; }
        [Column("kategoriId")]
        public int? categoryId { get; set; }
        [Column("kullaniciId")]
        public int? userId { get; set; }
        [Column("gununYemegimi")]
        public bool? isMealOfDay { get; set; }
    }
}

