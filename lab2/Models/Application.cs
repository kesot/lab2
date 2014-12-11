using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace lab2.Models
{
	public class Application
	{
		public int Id { get; set; }
		public string ClientIdentifier { get; set; }
		public string ClientSecret { get; set; }
		public string RedirectUrl { get; set; }
		public string Name { get; set; }


		[InverseProperty("Application")]
		public ICollection<Token> Tokens { get; set; }
	}
}