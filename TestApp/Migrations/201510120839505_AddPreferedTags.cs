namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreferedTags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tags", "ApplicationUserId");
            AddForeignKey("dbo.Tags", "ApplicationUserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "ApplicationUserId", "dbo.Users");
            DropIndex("dbo.Tags", new[] { "ApplicationUserId" });
            DropColumn("dbo.Tags", "ApplicationUserId");
        }
    }
}
