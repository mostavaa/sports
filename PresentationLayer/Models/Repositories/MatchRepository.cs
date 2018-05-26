using System;
using System.Collections.Generic;
using System.Linq;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class MatchRepository : Repository<Match>
    {
        public MatchRepository(ApplicationDbContext context) : base(context)
        {

        }

        public List<Match> GetMatches(DateTime dateTime)
        {
            return Get(o => o.MatchDateTime.Year == dateTime.Year && o.MatchDateTime.Month == dateTime.Month && o.MatchDateTime.Day == dateTime.Day).ToList();
        }
    }
}