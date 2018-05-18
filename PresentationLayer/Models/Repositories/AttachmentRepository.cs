using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class AttachmentRepository:Repository<Attachment>
    {
       
        public AttachmentRepository(ApplicationDbContext context) : base(context)
        {

        }


    }
}