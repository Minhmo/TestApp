namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Music_Id", "dbo.Musics");
            DropIndex("dbo.Tags", new[] { "Music_Id" });
       
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "MusicId", "dbo.Musics");
            DropIndex("dbo.Tags", new[] { "MusicId" });
            AlterColumn("dbo.Tags", "MusicId", c => c.Int());
            RenameColumn(table: "dbo.Tags", name: "MusicId", newName: "Music_Id");
            CreateIndex("dbo.Tags", "Music_Id");
            AddForeignKey("dbo.Tags", "Music_Id", "dbo.Musics", "Id");
        }
    }
}
