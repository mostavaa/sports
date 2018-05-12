using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class AlbumRepository:Repository<Album>
    {
        public ApplicationDbContext Context { get; }

        public AlbumRepository()
        {
           
        }
        public AlbumRepository(ApplicationDbContext ctx):this()
        {
            Context = ctx;
        }

       
    }
}