using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using lab2.Models;

namespace lab2.Controllers
{
	public class ApiTicketsController : ApiController
	{
		private MyShopDbContext db = new MyShopDbContext();

		
		// GET api/ApiTickets
		public IHttpActionResult GetTickets(string token)
		{
			var tokenRow = db.Tokens.SingleOrDefault(t => t.TokenString == token);
			if (tokenRow == null)
				return BadRequest();
			if (tokenRow.Expired())
				return Json(new []{"Token Expired"});
			var userid = tokenRow.UserId;
			var tickets = db.Tickets.Where(t => t.UserId == userid);
			var result = Json(tickets);
			return result;
		}


		// GET api/ApiTickets/5
		[ResponseType(typeof(Ticket))]
		public IHttpActionResult GetTicket(int id, string token)
		{
			var tokenRow = db.Tokens.SingleOrDefault(t => t.TokenString == token);
			if (tokenRow == null || tokenRow.Expired())
				return BadRequest();
			var userid = tokenRow.UserId;
			var ticket = db.Tickets.Where(t => t.UserId == userid).SingleOrDefault(t => t.Id == id);
			if (ticket == null)
			{
				return NotFound();
			}

			return Ok(ticket);
		}

		// PUT api/ApiTickets/5
		public IHttpActionResult PutTicket(int id, Ticket ticket)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != ticket.Id)
			{
				return BadRequest();
			}

			db.Entry(ticket).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TicketExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST api/ApiTickets
		[ResponseType(typeof(Ticket))]
		public IHttpActionResult PostTicket(Ticket ticket)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Tickets.Add(ticket);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = ticket.Id }, ticket);
		}

		// DELETE api/ApiTickets/5
		[ResponseType(typeof(Ticket))]
		public IHttpActionResult DeleteTicket(int id)
		{
			Ticket ticket = db.Tickets.Find(id);
			if (ticket == null)
			{
				return NotFound();
			}

			db.Tickets.Remove(ticket);
			db.SaveChanges();

			return Ok(ticket);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TicketExists(int id)
		{
			return db.Tickets.Count(e => e.Id == id) > 0;
		}
	}
}