using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class GoalRepository:Repository<Goal>
    {
        public ApplicationDbContext Context { get; }

        public GoalRepository()
        {
           
        }
        public GoalRepository(ApplicationDbContext ctx):this()
        {
            Context = ctx;
        }

       
    }
}