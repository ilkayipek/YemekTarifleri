using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YemekTarifleri.Models
{
	public class RegisterViewModel
	{
        public String userName { get; set; }
        public String? password { get; set; }
        public String? againPassword { get; set; }
        public String? email { get; set; }
        public String? nameAndSurname { get; set; }
        public bool gender { get; set; }
        public bool provisions { get; set; }
    }
}

