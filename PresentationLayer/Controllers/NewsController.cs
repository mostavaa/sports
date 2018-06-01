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
    public class NewsController : BaseController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            return View(_db.News.ToList());
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.Championships = AllChampionships();
            ViewBag.Matches = AllMatches();
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( News news)
        {
            if (ModelState.IsValid)
            {
                
                _db.News.Add(news);
                _db.SaveChanges();
                UploadImage(news.GUID);

                return RedirectToAction("Index");
            }
            ViewBag.Championships = AllChampionships(selectedChampionshipId: news.ChampionshipId ?? 0);
            ViewBag.Matches = AllMatches(selectedMatchId: news.MatchId?? 0);
            return View(news);
        }


        // GET: News/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Championships = AllChampionships(selectedChampionshipId: news.ChampionshipId ?? 0);
            ViewBag.Matches = AllMatches(selectedMatchId: news.MatchId ?? 0);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News Obj)
        {

            if (ModelState.IsValid)
            {
                UnitOfWork uow = new UnitOfWork();
                News ObjUpdated = uow.NewsRepository.GetById(Obj.Id);
                ObjUpdated.ChampionshipId = Obj.ChampionshipId;
                ObjUpdated.MatchId = Obj.MatchId;
                ObjUpdated.NewsHeading = Obj.NewsHeading;
                ObjUpdated.NewsDescription = Obj.NewsDescription;
                ObjUpdated.Stars = Obj.Stars;
                uow.NewsRepository.Update(ObjUpdated);
                uow.Save();
                UploadImage(ObjUpdated.GUID);
                return RedirectToAction("Index");
            }
            ViewBag.Championships = AllChampionships(selectedChampionshipId: Obj.ChampionshipId ?? 0);
            ViewBag.Matches = AllMatches(selectedMatchId: Obj.MatchId ?? 0);
            return View(Obj);
        }

        // GET: News/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            News news = _db.News.Find(id);
            if (news != null) _db.News.Remove(news);
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
