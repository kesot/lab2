using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using lab2.Models;
using lab2.ViewModels;
using PerpetuumSoft.Knockout;

namespace lab2.Controllers
{
	public class UsersController : Controller
	{
		private MyShopDbContext db = new MyShopDbContext();

		[HttpGet]
		public ActionResult Register()
		{
			return View("Create", new UsersViewModel());
		}

		public ActionResult DisableEmail(UsersViewModel model)
		{
			model.ShowEmail = false;
			return Json(model);
		}

		[HttpPost]
		public ActionResult Register(UsersViewModel request)
		{
			request.Save(db);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult Authorize()
		{
			return View("Authorize");
		}

		[HttpPost]
		public ActionResult Authorize(UsersViewModel request)
		{
			var sessionid = request.Authorize(db);
			if (sessionid != null)
			{
				Session["userid"] = db.Users.Single(u => u.Login == request.UserName).Id;
				Session["sessionid"] = sessionid;
				TempData["infMessage"] = "success";
			}
			else
			{
				TempData["infMessage"] = "denied";
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult AboutMe()
		{
			if (Session["userid"] != null)
			{
				var userModel = new UsersViewModel();
				var id = (int) Session["userid"];
				userModel.LoadData(db.Users.Single(u => u.Id == id ));
				return View("AboutMe", userModel);
			}
			TempData["infMessage"] = "needAuthorize";

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult AuthorizeApplication(string ClientId)
		{
			return View("AuthorizeApplication", (object)ClientId);
		}

		[HttpPost]
		public ActionResult AuthorizeApplicationSubmit(string ClientId)
		{
			if (Session["userid"] != null)
			{
				var ap = db.Applications.SingleOrDefault(a => a.ClientIdentifier == ClientId);
				var code = BitConverter.ToString(
					MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Session["userid"].ToString() + DateTime.Now.ToString()))).Replace("-",string.Empty);
				db.Tokens.AddOrUpdate(token => token.ApplicationId,
					new Token()
				{
					ApplicationId = ap.Id,
					UserId = (int) Session["userid"],
					Code = code
				});
				db.SaveChanges();
				TempData["infMessage"] = "application authorized";
				return Redirect(ap.RedirectUrl + "?code=" + code);
			}
			TempData["infMessage"] = "you need authorize";
			return RedirectToAction("Index", "Home");
		}
	}
}
