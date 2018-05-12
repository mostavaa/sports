using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models.Repositories
{
    public class AttachmentRepository:Repository<Attachment>
    {
        public ApplicationDbContext Context { get; }

        public AttachmentRepository()
        {
           
        }
        public AttachmentRepository(ApplicationDbContext ctx):this()
        {
            Context = ctx;
        }

       
    }
}