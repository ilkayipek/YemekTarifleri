using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YemekTarifleri.Models
{
	[Table("Tbl_Yorumlar")]
	public class Comment
	{
		[Key]
		[Column("yorumId")]
		public int? id { get; set; }
        [Column("yorumTarih")]
        public string? date { get; set; }
        [Column("yorumOnay")]
        public bool? approvalStatus { get; set; }
        [Column("yorumIcerik")]
        public string? contents { get; set; }
        [Column("tarifId")]
        public int? recipeId { get; set; }
        [Column("kullaniciId")]
        public int? userId { get; set; }
        [Column("yorumPuan")]
        public double? point { get; set; }

        public User user { get; set; }
        public Recipe recipe { get; set; }
    }
}

