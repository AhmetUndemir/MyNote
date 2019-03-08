using MyEvernote.Common.Helpers;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
	public class EvernoteUserManager
	{
		private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();


		public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
		{

			EvernoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
			BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

			if (user != null)
			{
				if (user.Username == data.Username)
				{
					res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı");
				}
				if (user.Email == data.Email)
				{
					res.AddError(ErrorMessageCode.EmailAlreadyExists, "Eposta Adresi Kayıtlı");
				}

			}
			else
			{
				int dbResult = repo_user.Insert(new EvernoteUser()
				{
					Username = data.Username,
					Email = data.Email,
					Password = data.Password,
					ActivateGuid = Guid.NewGuid(),
					IsActive = false,
					IsAdmin = false

				});

				if (dbResult > 0)
				{
					res.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

					//layerResult.Result.ActivateGuid
					string siteUri = ConfigHelper.Get<string>("SiteRootUri");
					string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
					string body = $"Merhaba {res.Result.Username} Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>";

					MailHelper.SendMail(body,res.Result.Email,"MyEvernote Hesap Aktifleştirme");

				}
			}

			return res;
		}

		public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
		{
			//Giriş kontrolü
			//Hesap aktive edilmiş mi?



			BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
			res.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);



			if (res.Result != null)
			{
				if (!res.Result.IsActive)
				{
					res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Aktifleştilmemiştir");
					res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen E-Posta Adresinizi Kontrol Ediniz");
				}

			}
			else
			{
				res.AddError(ErrorMessageCode.UsernameOrPasswrong, "Kullanıcı Adı yada şifre uyuşmuyor.");
			}



			return res;
		}

		public BusinessLayerResult<EvernoteUser> ActivateUser(Guid activateId)
		{
			BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
			res.Result = repo_user.Find(x => x.ActivateGuid == activateId);

			if (res.Result != null)
			{
				if (res.Result.IsActive)
				{
					res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı Zaten Aktif Edilmiştir");
					return res;
				}

				res.Result.IsActive = true;
				repo_user.Update(res.Result);
			}
			else
			{
				res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
			}
			return res;

		}
	}
}
