using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace lab2.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Time { get; set; }
		
		public string ArtistName { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }
		
		public User User { get; set; }
	}
	
}