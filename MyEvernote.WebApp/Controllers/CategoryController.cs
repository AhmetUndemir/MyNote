﻿using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: TempData ile Category Listeleme...
   //     public ActionResult Select(int? id)
   //     {
			//if (id == null)
			//{
			//	return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//}

			//CategoryManager cm = new CategoryManager();
			//Category cat = cm.GetCategoryById(id.Value);

			//if (cat == null)
			//{
			//	return HttpNotFound();
			//	//return RedirectToAction("Index", "Home");
			//}

			//TempData["model"] = cat.Notes;

			//return RedirectToAction("Index", "Home");
   //     }
    }
}