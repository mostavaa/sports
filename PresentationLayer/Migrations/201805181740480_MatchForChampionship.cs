namespace PresentationLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchForChampionship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "ChampionshipId", c => c.Long());
            CreateIndex("dbo.Matches", "ChampionshipId");
            AddForeignKey("dbo.Matches", "ChampionshipId", "dbo.Championships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "ChampionshipId", "dbo.Championships");
            DropIndex("dbo.Matches", new[] { "ChampionshipId" });
            DropColumn("dbo.Matches", "ChampionshipId");
        }
    }
}
