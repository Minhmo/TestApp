namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMusicIdIntags : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tags", "MusicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "MusicId", c => c.Int(nullable: false));
        }
    }
}
