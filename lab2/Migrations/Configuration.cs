using lab2.Models;

namespace lab2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<lab2.Models.MyShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(lab2.Models.MyShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
			context.Users.AddOrUpdate(u => u.Login,
				new User{ Email = "kesot@mail.ru", PasswordHash = "cooler".GetHashCode().ToString(), Login = "konst"});
			context.Tickets.AddOrUpdate(t => t.Name,
				new Ticket { ArtistName = "Winston", Name = "Piano Nova", Time = new DateTime(2013, 11, 11), UserId = 1 },
				new Ticket { ArtistName = "Scorpions", Name = "AndTurtle", Time = new DateTime(2013, 3, 11), UserId = 1 });
			context.Applications.AddOrUpdate(a => a.ClientIdentifier,
				new Application() { ClientIdentifier = "111111", ClientSecret = "hjlgaiuveb", Name = "SuperApp", RedirectUrl = "http://localhost/" });
		}
	}
}
