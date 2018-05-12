using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsNews.Models.DB
{
    public class Entity
    {
        public Entity()
        {
            GUID = Guid.NewGuid();
            CreatedTime = DateTime.Now;
        }
        [Key]
        public long Id { get; set; }
        public Guid GUID { get; set; }
        public DateTime  CreatedTime { get; set; }
        public string CreatedBy { get; set; }
    }
}