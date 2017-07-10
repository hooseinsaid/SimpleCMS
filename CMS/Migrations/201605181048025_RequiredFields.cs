namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsStories", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.NewsStories", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsStories", "Content", c => c.String());
            AlterColumn("dbo.NewsStories", "Title", c => c.String());
        }
    }
}
