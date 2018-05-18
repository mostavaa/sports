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
    public class GoalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Goals
        public ActionResult Index()
        {
            return View(db.Goals.ToList());
        }

        // GET: Goals/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal Goal = db.Goals.Find(id);
            if (Goal == null)
            {
                return HttpNotFound();
            }
            return View(Goal);
        }

        // GET: Goals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Goal Goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(Goal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Goal);
        }

        // GET: Goals/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal Goal = db.Goals.Find(id);
            if (Goal == null)
            {
                return HttpNotFound();
            }
            return View(Goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Goal Goal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Goal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Goal);
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
