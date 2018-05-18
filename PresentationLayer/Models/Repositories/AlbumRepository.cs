using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class AlbumRepository:Repository<Album>
    {

        public AlbumRepository(ApplicationDbContext context):base(context)
        {

        }
       
    }
}