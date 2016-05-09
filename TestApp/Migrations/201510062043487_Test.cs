namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {

            DropForeignKey("dbo.Musics", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.Musics", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Music_Id", "dbo.Musics");
            DropIndex("dbo.Users", new[] { "Music_Id" });
            DropIndex("dbo.Musics", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Musics", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.UserMusic",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        MusicRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.MusicRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Musics", t => t.MusicRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.MusicRefId);
            
   
        }
        
        public override void Down()
        {
           
        }
    }
}
