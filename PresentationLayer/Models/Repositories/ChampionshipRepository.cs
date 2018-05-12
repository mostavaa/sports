using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class ChampionshipRepository:Repository<Championship>
    {
        public ApplicationDbContext Context { get; }

        public ChampionshipRepository()
        {
           
        }
        public ChampionshipRepository(ApplicationDbContext ctx):this()
        {
            Context = ctx;
        }

       
    }
}