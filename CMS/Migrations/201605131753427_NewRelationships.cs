namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KeyWords", "NewsStoryID", "dbo.NewsStories");
            DropIndex("dbo.KeyWords", new[] { "NewsStoryID" });
            RenameColumn(table: "dbo.Images", name: "ImageID", newName: "NewsStoryID");
            RenameIndex(table: "dbo.Images", name: "IX_ImageID", newName: "IX_NewsStoryID");
            CreateTable(
                "dbo.KeyWordNewsStories",
                c => new
                    {
                        KeyWord_KeyWordID = c.Int(nullable: false),
                        NewsStory_NewsStoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.KeyWord_KeyWordID, t.NewsStory_NewsStoryID })
                .ForeignKey("dbo.KeyWords", t => t.KeyWord_KeyWordID, cascadeDelete: true)
                .ForeignKey("dbo.NewsStories", t => t.NewsStory_NewsStoryID, cascadeDelete: true)
                .Index(t => t.KeyWord_KeyWordID)
                .Index(t => t.NewsStory_NewsStoryID);
            
            DropColumn("dbo.KeyWords", "NewsStoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KeyWords", "NewsStoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.KeyWordNewsStories", "NewsStory_NewsStoryID", "dbo.NewsStories");
            DropForeignKey("dbo.KeyWordNewsStories", "KeyWord_KeyWordID", "dbo.KeyWords");
            DropIndex("dbo.KeyWordNewsStories", new[] { "NewsStory_NewsStoryID" });
            DropIndex("dbo.KeyWordNewsStories", new[] { "KeyWord_KeyWordID" });
            DropTable("dbo.KeyWordNewsStories");
            RenameIndex(table: "dbo.Images", name: "IX_NewsStoryID", newName: "IX_ImageID");
            RenameColumn(table: "dbo.Images", name: "NewsStoryID", newName: "ImageID");
            CreateIndex("dbo.KeyWords", "NewsStoryID");
            AddForeignKey("dbo.KeyWords", "NewsStoryID", "dbo.NewsStories", "NewsStoryID", cascadeDelete: true);
        }
    }
}
