using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class MatchRepository:Repository<Match>
    {
        public ApplicationDbContext Context { get; }

        public MatchRepository()
        {
           
        }
        public MatchRepository(ApplicationDbContext ctx):this()
        {
            Context = ctx;
        }

       
    }
}