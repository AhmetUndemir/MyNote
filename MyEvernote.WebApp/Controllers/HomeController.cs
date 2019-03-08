using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			//CategoryController üzerinden gelen view talebi ve model...
			//if (TempData["model"] != null)
			//{
			//	return View(TempData["model"] as List<Note>);
			//}

			NoteManager nm = new NoteManager();

			return View(nm.GetAllNotes().OrderByDescending(x => x.ModifiedOn).ToList());
			//return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());


		}

		public ActionResult ByCategory(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			CategoryManager cm = new CategoryManager();
			Category cat = cm.GetCategoryById(id.Value);

			if (cat == null)
			{
				return HttpNotFound();
				//return RedirectToAction("Index", "Home");
			}

			return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
		}

		public ActionResult MostLiked()
		{
			NoteManager nm = new NoteManager();

			return View("Index", nm.GetAllNotes().OrderByDescending(x => x.LikeCount).ToList());
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();

		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				EvernoteUserManager eum = new EvernoteUserManager();
				BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

				if (res.Errors.Count > 0)
				{
					if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
					{
						ViewBag.SetLink = "http://Home/Activate/1234-4567-78980";
					}


					res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));


					return View(model);
				}

				Session["login"] = res.Result; //session'a kullanıcı bilgisini saklama..
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				EvernoteUserManager eum = new EvernoteUserManager();
				BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);
				if (res.Errors.Count > 0)
				{
					res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

					return View(model);
				}


				return RedirectToAction("RegisterOk");

			}

			return View(model);
		}

		public ActionResult RegisterOk()
		{
			return View();
		}

		public ActionResult UserActivate(Guid id)
		{
			//kullanıcı aktifleştirme
			EvernoteUserManager eum = new EvernoteUserManager();
			BusinessLayerResult<EvernoteUser> res = eum.ActivateUser(id);

			if (res.Errors.Count > 0)
			{
				TempData["errors"] = res.Errors;
				return RedirectToAction("UserActivateCancel");
			}


			return RedirectToAction("UserActivateOK");
		}

		public ActionResult UserActivateOK()
		{

			return View();
		}

		public ActionResult UserActivateCancel()
		{
			List<ErrorMessageObj> errors = null;

			if (TempData["errors"] != null)
			{
				errors = TempData["errors"] as List<ErrorMessageObj>;
			}

			return View(errors);
		}

		public ActionResult Logout()
		{
			Session.Clear();

			return RedirectToAction("Index");
		}


	}
}