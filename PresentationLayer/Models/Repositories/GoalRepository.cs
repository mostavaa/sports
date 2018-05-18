using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class GoalRepository:Repository<Goal>
    {
     
        public GoalRepository(ApplicationDbContext context) : base(context)
        {

        }


    }
}