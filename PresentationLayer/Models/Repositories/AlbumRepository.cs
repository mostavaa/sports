using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class AlbumRepository:Repository<Album>
    {

        public AlbumRepository(ApplicationDbContext context):base(context)
        {

        }

        public List<Album> GetAlbums(int offset)
        {
            return  Get().Skip(offset).Take(Common.DefaultTake).ToList();

        }
    }
}