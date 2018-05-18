using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.dbModels;
using PresentationLayer.Models.Repositories;

namespace PresentationLayer.Controllers
{
    public class GoalsController : BaseController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Goals
        public ActionResult Index()
        {
            return View(_db.Goals.ToList());
        }

     
        // GET: Goals/Create
        public ActionResult Create()
        {
            ViewBag.Championships = AllChampionships();
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Goal goal)
        {
            
            if (ModelState.IsValid)
            {
                UnitOfWork uow = new UnitOfWork();
                uow.GoalRepository.Insert(goal);
                uow.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Championships = AllChampionships();
            
            return View(goal);
        }

        // GET: Goals/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = _db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Championships = AllChampionships(selectedChampionshipId: goal.Match.ChampionshipId ?? 0);
            
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Goal goal)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(goal).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Championships = AllChampionships(selectedChampionshipId: goal.Match.ChampionshipId ?? 0);
            //ViewBag.Matches = AllMatches(addNullEntry: false, selectedMatchId: goal.MatchId, ChampionshipId: goal.Match?.ChampionshipId);
            return View(goal);
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
