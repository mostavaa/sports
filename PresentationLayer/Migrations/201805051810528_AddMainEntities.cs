namespace PresentationLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMainEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AlbumName = c.String(),
                        MatchId = c.Long(),
                        NewsId = c.Long(),
                        GUID = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .ForeignKey("dbo.News", t => t.NewsId)
                .Index(t => t.MatchId)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AlbumId = c.Long(nullable: false),
                        GUID = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstTeamName = c.String(),
                        SecondTeamName = c.String(),
                        FirstTeamGoals = c.Int(nullable: false),
                        SecondTeamGoals = c.Int(nullable: false),
                        MatchDateTime = c.DateTime(nullable: false),
                        IsPlayed = c.Boolean(nullable: false),
                        GUID = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GoalLink = c.String(),
                        GoalPlayerName = c.String(),
                        MatchId = c.Long(nullable: false),
                        GUID = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.MatchId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NewsHeading = c.String(),
                        NewsDescription = c.String(),
                        Stars = c.Int(nullable: false),
                        ChampionshipId = c.Long(),
                        MatchId = c.Long(),
                        GUID = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Championships", t => t.ChampionshipId)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .Index(t => t.ChampionshipId)
                .Index(t => t.MatchId);
            
            CreateTable(
                "dbo.Championships",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChampName = c.String(),
                        Stars = c.Int(nullable: false),
                        GUID = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.News", "ChampionshipId", "dbo.Championships");
            DropForeignKey("dbo.Albums", "NewsId", "dbo.News");
            DropForeignKey("dbo.Goals", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Albums", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Attachments", "AlbumId", "dbo.Albums");
            DropIndex("dbo.News", new[] { "MatchId" });
            DropIndex("dbo.News", new[] { "ChampionshipId" });
            DropIndex("dbo.Goals", new[] { "MatchId" });
            DropIndex("dbo.Attachments", new[] { "AlbumId" });
            DropIndex("dbo.Albums", new[] { "NewsId" });
            DropIndex("dbo.Albums", new[] { "MatchId" });
            DropTable("dbo.Championships");
            DropTable("dbo.News");
            DropTable("dbo.Goals");
            DropTable("dbo.Matches");
            DropTable("dbo.Attachments");
            DropTable("dbo.Albums");
        }
    }
}
