using SportsNews.Models.DB;

namespace PresentationLayer.Models.dbModels
{
    public class Goal: Entity
    {
        public string GoalLink { get; set; }
        public string GoalPlayerName { get; set; }



        public long MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}