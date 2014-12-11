namespace lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientIdentifier = c.String(),
                        ClientSecret = c.String(),
                        RedirectUrl = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                        TokenString = c.String(),
                        TokenRepair = c.String(),
                        TokenExpirationDateTimeUtc = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Time = c.DateTime(nullable: false),
                        ArtistName = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Tokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Tokens", new[] { "ApplicationId" });
            DropIndex("dbo.Tokens", new[] { "UserId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Users");
            DropTable("dbo.Tokens");
            DropTable("dbo.Applications");
        }
    }
}
