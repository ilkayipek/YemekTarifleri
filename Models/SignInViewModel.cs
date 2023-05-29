using System;
namespace YemekTarifleri.Models
{
	public class SignInViewModel
	{
		public string? emailOrUsername { get; set; }
		public string? password { get; set; }
		public bool IsSubmitted { get; set; }

    }
}

