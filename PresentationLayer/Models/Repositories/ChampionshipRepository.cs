using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class ChampionshipRepository : Repository<Championship>
    {
        public ChampionshipRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}