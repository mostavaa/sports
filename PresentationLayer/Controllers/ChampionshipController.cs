using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Controllers
{
    public class ChampionshipController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Championships
        public ActionResult Index()
        {
            return View(db.Championships.ToList());
        }

        // GET: Championships/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship Championship = db.Championships.Find(id);
            if (Championship == null)
            {
                return HttpNotFound();
            }
            return View(Championship);
        }

        // GET: Championships/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Championships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstTeamName,SecondTeamName,FirstTeamGoals,SecondTeamGoals,ChampionshipDateTime,IsPlayed,GUID,CreatedTime,CreatedBy")] Championship Championship)
        {
            if (ModelState.IsValid)
            {
                db.Championships.Add(Championship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Championship);
        }

        // GET: Championships/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship Championship = db.Championships.Find(id);
            if (Championship == null)
            {
                return HttpNotFound();
            }
            return View(Championship);
        }

        // POST: Championships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstTeamName,SecondTeamName,FirstTeamGoals,SecondTeamGoals,ChampionshipDateTime,IsPlayed,GUID,CreatedTime,CreatedBy")] Championship Championship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Championship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Championship);
        }

        // GET: Championships/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship Championship = db.Championships.Find(id);
            if (Championship == null)
            {
                return HttpNotFound();
            }
            return View(Championship);
        }

        // POST: Championships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Championship Championship = db.Championships.Find(id);
            db.Championships.Remove(Championship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
