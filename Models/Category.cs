using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YemekTarifleri.Models
{
    [Table("Tbl_Kategoriler")]
    public class Category
    {
        [Key]
        [Column("kategoriId")]
        public int? id { get; set; }
        [Column("kategoriAd")]
        public String? name { get; set; }
        [Column("kategoriAdet")]
        public int? piece { get; set; }
        [Column("kategoriResim")]
        public String? image { get; set; }

        public List<Recipe>? recipes { get; set; }
    }
}