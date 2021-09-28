namespace ZavenDotNetInterview.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeJobStateName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "State", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "Status");
        }
    }
}
