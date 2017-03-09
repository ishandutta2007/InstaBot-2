namespace TestADBManagement.BotTasks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestoreDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InstagramAccounts", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InstagramAccounts", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
