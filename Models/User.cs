using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace YemekTarifleri.Models
{
    [Table("Tbl_Kullanicilar")]
    public class User 
    {
        [Key]
        [Column("kullaniciId")]
        public int? id { get; set; }
        [Column("kullaniciKullaniciAdi")]
        public String? userName { get; set; }
        [Column("kullaniciSifre")]
        public String? password { get; set; }
        [Column("kullaniciMail")]
        public String? email { get; set; }
        [Column("kullaniciAdSoyad")]
        public String? nameAndSurname { get; set; }
        [Column("kullaniciTipi")]
        public Boolean? userType { get; set; }
        [Column("kullaniciAvatar")]
        public String? avatar { get; set; }

        public List<Recipe> recipes { get; set; }
        public List<Comment>? comments { get; set; }
    }
}
