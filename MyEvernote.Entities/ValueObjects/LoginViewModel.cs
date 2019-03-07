using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObjects
{
	public class LoginViewModel
	{
		[Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		[StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
		public string Username { get; set; }

		[Display(Name = "Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		[StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
		public string Password { get; set; }
	}
}