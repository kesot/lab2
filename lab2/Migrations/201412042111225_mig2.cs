namespace lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tokens", "Code", c => c.String());
            AlterColumn("dbo.Tokens", "TokenExpirationDateTimeUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tokens", "TokenExpirationDateTimeUtc", c => c.String());
            DropColumn("dbo.Tokens", "Code");
        }
    }
}
