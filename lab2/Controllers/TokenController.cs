using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using lab2.Models;

namespace lab2.Controllers
{
    public class TokenController : ApiController
    {
        private MyShopDbContext db = new MyShopDbContext();

        // GET api/Token
		public IHttpActionResult GetToken(string clientid, string clientsecret, string code)
		{
			var tokenrow = db.Tokens.Where(
				t => t.Code == code).Join(db.Applications, t => t.ApplicationId, a => a.Id, (token, application) =>
					(application.ClientIdentifier == clientid && application.ClientSecret == clientsecret) ? token : null).SingleOrDefault();
			if (tokenrow == null)
				return BadRequest();
			var tokenstring = BitConverter.ToString(
					MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(tokenrow.Code + tokenrow.UserId + DateTime.Now.ToString()))).Replace("-","");
			tokenrow.TokenString = tokenstring;
			tokenrow.Code = "puhad";
			tokenrow.TokenExpirationDateTimeUtc = DateTime.UtcNow.AddHours(1);
			tokenrow.TokenRepair = BitConverter.ToString(
					MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(tokenrow.Code + DateTime.Now.AddHours(1) + tokenrow.UserId + DateTime.Now.ToString()))).Replace("-","");
			var response = this.Request.CreateResponse(HttpStatusCode.OK);
			db.SaveChanges();
			return Json(new { Data = new { token = tokenstring, expiration = tokenrow.TokenExpirationDateTimeUtc } });
		}

		public IHttpActionResult GetNewToken(string refreshtoken)
		{
			var tokenrow = db.Tokens.SingleOrDefault(
				t => t.TokenRepair == refreshtoken);
			if (tokenrow == null)
				return BadRequest();
			var tokenstring = BitConverter.ToString(
					MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(tokenrow.TokenRepair + tokenrow.UserId + DateTime.Now.ToString()))).Replace("-", "");
			tokenrow.TokenString = tokenstring;
			tokenrow.TokenExpirationDateTimeUtc = DateTime.UtcNow.AddHours(1);
			tokenrow.TokenRepair = BitConverter.ToString(
					MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(tokenrow.Code + DateTime.Now.AddHours(1) + tokenrow.UserId + DateTime.Now.ToString()))).Replace("-", "");
			var response = this.Request.CreateResponse(HttpStatusCode.OK);
			db.SaveChanges();
			return Json(new { Data = new { token = tokenstring, expiration = tokenrow.TokenExpirationDateTimeUtc } });
		}
    }
}