namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NtoNTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "MusicId", "dbo.Musics");
            DropIndex("dbo.Tags", new[] { "MusicId" });
            CreateTable(
                "dbo.TagMusics",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        MusicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagId, t.MusicId })
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.Musics", t => t.MusicId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.MusicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagMusics", "Music_Id", "dbo.Musics");
            DropForeignKey("dbo.TagMusics", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagMusics", new[] { "Music_Id" });
            DropIndex("dbo.TagMusics", new[] { "Tag_Id" });
            DropTable("dbo.TagMusics");
            CreateIndex("dbo.Tags", "MusicId");
            AddForeignKey("dbo.Tags", "MusicId", "dbo.Musics", "Id", cascadeDelete: true);
        }
    }
}
