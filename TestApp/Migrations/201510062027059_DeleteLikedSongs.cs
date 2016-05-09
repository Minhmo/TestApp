namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteLikedSongs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikedSongs", "MusicId", "dbo.Musics");
            DropForeignKey("dbo.LikedSongs", "UserId", "dbo.Users");
            DropIndex("dbo.LikedSongs", new[] { "UserId" });
            DropIndex("dbo.LikedSongs", new[] { "MusicId" });
            DropTable("dbo.LikedSongs");
            

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LikedSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        MusicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Musics", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.Musics", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Music_Id", "dbo.Musics");
            DropIndex("dbo.Users", new[] { "Music_Id" });
            DropIndex("dbo.Musics", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Musics", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Users", "Music_Id");
            DropColumn("dbo.Musics", "ApplicationUser_Id1");
            DropColumn("dbo.Musics", "ApplicationUser_Id");
            CreateIndex("dbo.LikedSongs", "MusicId");
            CreateIndex("dbo.LikedSongs", "UserId");
            AddForeignKey("dbo.Musics", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.LikedSongs", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.LikedSongs", "MusicId", "dbo.Musics", "Id", cascadeDelete: true);
        }
    }
}
