using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.dbModels;
using PresentationLayer.Models.Repositories;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Adminstration")]
    public class AlbumController : BaseController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Albums
        public ActionResult Index()
        {
            return View(_db.Albums.ToList());
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.Championships = AllChampionships();
            ViewBag.News = AllNews();
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Album Album)
        {
            if (ModelState.IsValid)
            {
                
                _db.Albums.Add(Album);
                _db.SaveChanges();
                UploadImage(Album.GUID);
                return RedirectToAction("Index");
            }
            ViewBag.Championships = AllChampionships(selectedChampionshipId: Album.Match.ChampionshipId ?? 0);
            ViewBag.News = AllNews();

            return View(Album);
        }


        // GET: Albums/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album Album = _db.Albums.Find(id);
            if (Album == null)
            {
                return HttpNotFound();
            }
            ViewBag.News = AllNews();
            ViewBag.Championships = AllChampionships(selectedChampionshipId: Album.Match.ChampionshipId ?? 0);
            return View(Album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Album Obj)
        {
            
            if (ModelState.IsValid)
            {
                UnitOfWork uow = new UnitOfWork();
                Album ObjUpdated =  uow.AlbumRepository.GetById(Obj.Id);
                ObjUpdated.AlbumName = Obj.AlbumName;
                ObjUpdated.MatchId = Obj.MatchId;
                ObjUpdated.NewsId = Obj.NewsId;
                uow.AlbumRepository.Update(ObjUpdated);
                uow.Save();
                UploadImage(ObjUpdated.GUID);
                return RedirectToAction("Index");
            }
            ViewBag.News = AllNews();
            ViewBag.Championships = AllChampionships(selectedChampionshipId: Obj.Match.ChampionshipId ?? 0);
            return View(Obj);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album Album = _db.Albums.Find(id);
            if (Album == null)
            {
                return HttpNotFound();
            }
            return View(Album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Album Album = _db.Albums.Find(id);
            if (Album != null) _db.Albums.Remove(Album);
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
