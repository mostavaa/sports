using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.dbModels;
using PresentationLayer.Models.Repositories;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Adminstration")]
    public class AttachmentController : BaseController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Attachments
        public ActionResult Index()
        {
            return View(_db.Attachments.ToList());
        }

        // GET: Attachments/Create
        public ActionResult Create()
        {
            ViewBag.Albums = AllAlbums(addNullEntry: false);
            return View();
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                
                _db.Attachments.Add(attachment);
                _db.SaveChanges();
              UploadImage(attachment.GUID);
                return RedirectToAction("Index");
            }
            ViewBag.Albums = AllAlbums(addNullEntry: false , selectedAlbumId:attachment.AlbumId);
            return View(attachment);
        }


        // GET: Attachments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = _db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Albums =  AllAlbums(addNullEntry: false, selectedAlbumId: attachment.AlbumId);
            return View(attachment);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Attachment Obj)
        {

            if (ModelState.IsValid)
            {
                UnitOfWork uow = new UnitOfWork();
                Attachment ObjUpdated = uow.AttachmentRepository.GetById(Obj.Id);
                ObjUpdated.AlbumId = Obj.AlbumId;
                uow.AttachmentRepository.Update(ObjUpdated);
                uow.Save();
                UploadImage(ObjUpdated.GUID);
                return RedirectToAction("Index");
            }
            ViewBag.Albums=AllAlbums(addNullEntry: false, selectedAlbumId: Obj.AlbumId);
            return View(Obj);
        }

        // GET: Attachments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = _db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Attachment attachment = _db.Attachments.Find(id);
            if (attachment != null) _db.Attachments.Remove(attachment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
