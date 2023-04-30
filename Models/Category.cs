using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrialWebApp.DataBase
{
    [Table("Tbl_Kategoriler")]
    public class Kategori
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
    }
}