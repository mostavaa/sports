using System.Collections.Generic;
using System.Linq;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class GoalRepository:Repository<Goal>
    {
     
        public GoalRepository(ApplicationDbContext context) : base(context)
        {

        }

        public List<Goal> GetGoals(int offset = 0, long MatchId = 0)
        {
            return MatchId > 0
                ? Get(o => o.MatchId == MatchId).Skip(offset).Take(Common.DefaultTake).ToList()
                : Get().Skip(offset).Take(Common.DefaultTake).ToList();


        }
    }
}