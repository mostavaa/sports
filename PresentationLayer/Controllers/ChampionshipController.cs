using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Controllers
{
    public class ChampionshipController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Championships
        public ActionResult Index()
        {
            return View(_db.Championships.ToList());
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
        public ActionResult Create( Championship championship)
        {
            if (ModelState.IsValid)
            {
                _db.Championships.Add(championship);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(championship);
        }

        // GET: Championships/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship championship = _db.Championships.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            return View(championship);
        }

        // POST: Championships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Championship championship)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(championship).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(championship);
        }

        // GET: Championships/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship championship = _db.Championships.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            return View(championship);
        }

        // POST: Championships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Championship championship = _db.Championships.Find(id);
            if (championship != null) _db.Championships.Remove(championship);
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
