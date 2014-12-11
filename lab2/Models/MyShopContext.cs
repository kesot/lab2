using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lab2.Models
{
	public class MyShopDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Application> Applications { get; set; }
		public DbSet<Token> Tokens { get; set; }
	}
}