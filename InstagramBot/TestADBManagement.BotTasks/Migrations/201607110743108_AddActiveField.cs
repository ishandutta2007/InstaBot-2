namespace TestADBManagement.BotTasks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveField : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstagramAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        InstagramPass = c.String(),
                        Email = c.String(),
                        EmailPass = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InstagramAccounts");
        }
    }
}
