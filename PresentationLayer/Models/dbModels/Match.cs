using System;
using System.Collections.Generic;
using SportsNews.Models.DB;

namespace PresentationLayer.Models.dbModels
{
    public class Match : Entity
    {
       
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public int FirstTeamGoals { get; set; }
        public int SecondTeamGoals { get; set; }
        public DateTime MatchDateTime { get; set; }
        public bool IsPlayed { get; set; }

        public long? ChampionshipId { get; set; }
        public virtual Championship Championship { get; set; }
        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();
        public virtual ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();
    }
}