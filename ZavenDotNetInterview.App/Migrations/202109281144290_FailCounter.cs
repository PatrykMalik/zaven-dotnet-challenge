namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FailCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "FailCounter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "FailCounter");
        }
    }
}
