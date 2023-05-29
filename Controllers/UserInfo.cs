using System;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.Models;
using YemekTarifleri.DataBase;

namespace YemekTarifleri.Controllers
{
	static public class UserInfo
	{
		private static User? user;
		static public void setUser(User? newUser) {
			user = newUser;
		}

		static public User? getUser()
		{
			return user;
		}
	}
}

