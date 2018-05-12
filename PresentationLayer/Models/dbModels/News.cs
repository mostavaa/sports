using System.Collections.Generic;
using SportsNews.Models.DB;

namespace PresentationLayer.Models.dbModels
{
    public class News:Entity
    {
      
        public string NewsHeading { get; set; }
        public string NewsDescription { get; set; }
        public int Stars { get; set; }



        public long? ChampionshipId { get; set; }
        public virtual Championship Championship{ get; set; }
        public long? MatchId { get; set; }
        public virtual Match Match { get; set; }
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}