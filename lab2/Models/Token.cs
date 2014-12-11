using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab2.Models
{
	public class Token
	{
		public int Id { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		[ForeignKey("Application")]
		public int ApplicationId { get; set; }
		public string TokenString { get; set; }
		public string TokenRepair { get; set; }
		public DateTime? TokenExpirationDateTimeUtc { get; set; }
		public string Code { get; set; }

		
		public User User { get; set; }
		public Application Application { get; set; }

		public bool Expired()
		{
			return TokenExpirationDateTimeUtc < DateTime.UtcNow;
		}

	}
}