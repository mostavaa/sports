using System.Collections.Generic;
using SportsNews.Models.DB;

namespace PresentationLayer.Models.dbModels
{
    public class Album: Entity
    {
        public string AlbumName { get; set; }


        public long? MatchId { get; set; }
        public virtual Match Match { get; set; }
        public long? NewsId { get; set; }
        public virtual News News { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}