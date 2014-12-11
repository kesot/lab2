namespace lab2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tokens", "TokenExpirationDateTimeUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tokens", "TokenExpirationDateTimeUtc", c => c.DateTime(nullable: false));
        }
    }
}
