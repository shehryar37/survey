using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyProject;

namespace SurveyProject.Controllers
{
    public class SectionsController : Controller
    {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: Sections
        public ActionResult Index()
        {
            var tblSections = db.tblSections.Include(t => t.tblClass);
            return View(tblSections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSection tblSection = db.tblSections.Find(id);
            if (tblSection == null)
            {
                return HttpNotFound();
            }
            return View(tblSection);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectionID,SectionName,ReportingDateTime,ClassID")] tblSection tblSection)
        {
            if (ModelState.IsValid)
            {
                tblSection.SectionActive = true;

                tblSection.ReportingDateTime = DateTime.Now;
                db.tblSections.Add(tblSection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName", tblSection.ClassID);
            return View(tblSection);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSection tblSection = db.tblSections.Find(id);
            if (tblSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName", tblSection.ClassID);
            return View(tblSection);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,SectionName,ClassID")] tblSection tblSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName", tblSection.ClassID);
            return View(tblSection);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSection tblSection = db.tblSections.Find(id);
            if (tblSection == null)
            {
                return HttpNotFound();
            }
            return View(tblSection);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSection tblSection = db.tblSections.Find(id);
            db.tblSections.Remove(tblSection);

            tblSection.SectionActive = false;

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
