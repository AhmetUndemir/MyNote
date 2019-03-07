using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObjects
{
	public class RegisterViewModel
	{
		[Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		[StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
		public string Username { get; set; }

		[Display(Name = "EPosta Adresi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		[StringLength(70, ErrorMessage = "{0} max. {1} karakter olmalı.")]
		[EmailAddress(ErrorMessage ="Lütfen {0} alanı için geçerli bir e-posta adresi giriniz.")]
		public string Email { get; set; }
		[Display(Name = "Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		[StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
		public string Password { get; set; }
		[Display(Name = "Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		[StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
		[Compare("Password",ErrorMessage = "{0} ile {1} uyuşmuyor.")]
		public string RePassword { get; set; }
	}
}