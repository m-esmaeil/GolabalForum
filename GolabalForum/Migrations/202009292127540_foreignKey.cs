namespace GolabalForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 150),
                        Body = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                        TotalViews = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastLoginAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        Body = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .Index(t => t.TopicId)
                .Index(t => t.CreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Comments", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Comments", "CreatedBy", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "CreatedBy" });
            DropIndex("dbo.Comments", new[] { "TopicId" });
            DropIndex("dbo.Topics", new[] { "CreatedBy" });
            DropIndex("dbo.Topics", new[] { "CategoryId" });
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Topics");
            DropTable("dbo.Categories");
        }
    }
}
