using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class MatchRepository:Repository<Match>
    {
        public MatchRepository(ApplicationDbContext context) : base(context)
        {

        }


    }
}