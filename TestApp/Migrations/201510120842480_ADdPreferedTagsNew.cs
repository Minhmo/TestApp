namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADdPreferedTagsNew : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tags", name: "ApplicationUserId", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Tags", name: "IX_ApplicationUserId", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
        }
    }
}
