using System.Collections.Generic;
using SportsNews.Models.DB;

namespace PresentationLayer.Models.dbModels
{
    public class Championship: Entity
    {
     
        public string ChampName { get; set; }
        public int Stars { get; set; }


        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
        public virtual ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    }
}