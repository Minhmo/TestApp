namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikedSongs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikedSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        MusicId = c.Int(),
                     
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Musics", t => t.MusicId)
                .Index(t => t.UserId)
                .Index(t => t.MusicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedSongs", "Music_Id", "dbo.Musics");
            DropForeignKey("dbo.LikedSongs", "UserId", "dbo.Users");
            DropIndex("dbo.LikedSongs", new[] { "Music_Id" });
            DropIndex("dbo.LikedSongs", new[] { "UserId" });
            DropTable("dbo.LikedSongs");
        }
    }
}
