namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false),
                        ImageName = c.String(),
                        ImageContent = c.Binary(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.NewsStories", t => t.ImageID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.NewsStories",
                c => new
                    {
                        NewsStoryID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Content = c.String(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.NewsStoryID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.KeyWords",
                c => new
                    {
                        KeyWordID = c.Int(nullable: false, identity: true),
                        NewsStoryID = c.Int(nullable: false),
                        keyWordContent = c.String(),
                    })
                .PrimaryKey(t => t.KeyWordID)
                .ForeignKey("dbo.NewsStories", t => t.NewsStoryID, cascadeDelete: true)
                .Index(t => t.NewsStoryID);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ImageID", "dbo.NewsStories");
            DropForeignKey("dbo.NewsStories", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KeyWords", "NewsStoryID", "dbo.NewsStories");
            DropIndex("dbo.KeyWords", new[] { "NewsStoryID" });
            DropIndex("dbo.NewsStories", new[] { "UserId" });
            DropIndex("dbo.Images", new[] { "ImageID" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.KeyWords");
            DropTable("dbo.NewsStories");
            DropTable("dbo.Images");
        }
    }
}
