using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace lab2.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }

		[InverseProperty ("User")]
		public ICollection<Ticket> Tickets { get; set; }
	}
	
}