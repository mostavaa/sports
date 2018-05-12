using System;
using System.ComponentModel.DataAnnotations;
using SportsNews.Models.DB;

namespace PresentationLayer.Models.dbModels
{
    public class Attachment:Entity
    {


        public long AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}