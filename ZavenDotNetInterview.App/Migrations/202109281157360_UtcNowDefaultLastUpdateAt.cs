namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UtcNowDefaultLastUpdateAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "CreatedAt");
        }
    }
}
