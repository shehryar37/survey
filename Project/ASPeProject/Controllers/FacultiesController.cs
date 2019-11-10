using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyProject;

namespace SurveyProject.Controllers {
    public class FacultiesController : Controller {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: Faculties
        public ActionResult Index() {
            var tblFaculties = db.tblFaculties.Include(t => t.tblUsers);
            return View(tblFaculties.Where(x => x.FacultyActive).ToList());
        }

        // GET: Faculties/Details/5
        public ActionResult Details(string id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            tblFaculty tblFaculty = db.tblFaculties.Find(id);
            if (tblFaculty == null) return HttpNotFound();
            
            return View(tblFaculty);
        }

        // GET: Faculties/Create
        public ActionResult Create() {
            ViewBag.FacultyID = new SelectList(db.tblUsers, "UserID", "UserName");
            return View();
        }

        // POST: Faculties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyName,FacultyEmail,FacultySpecification,FacultyGender")] tblFaculty tblFaculty) {
            if (ModelState.IsValid) {
                // Setting Active to true, because it is a new field.
                tblFaculty.FacultyActive = true;

                // Creating an ID for Faculty based on total records + 1.
                int count = db.tblFaculties.Count(); count++;
                tblFaculty.FacultyID = "faculty" + count;   // Setting the Faculty ID

                // Joining date set to current date.
                tblFaculty.FacultyJoiningDate = DateTime.Today.Date.ToShortDateString();

                db.tblFaculties.Add(tblFaculty);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.FacultyID = new SelectList(db.tblUsers, "UserID", "UserName", tblFaculty.FacultyID);
            return View(tblFaculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(string id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            tblFaculty tblFaculty = db.tblFaculties.Find(id);
            if (tblFaculty == null) return HttpNotFound();
            
            ViewBag.FacultyID = new SelectList(db.tblUsers, "UserID", "UserName", tblFaculty.FacultyID);
            return View(tblFaculty);
        }

        // POST: Faculties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyID,FacultyEmail,FacultySpecification,FacultyName,FacultyGender,FacultyJoiningDate")] tblFaculty tblFaculty) {
            if (ModelState.IsValid) {
                tblFaculty.FacultyActive = true;

                db.Entry(tblFaculty).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.FacultyID = new SelectList(db.tblUsers, "UserID", "UserName", tblFaculty.FacultyID);
            return View(tblFaculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(string id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            tblFaculty tblFaculty = db.tblFaculties.Find(id);
            if (tblFaculty == null) return HttpNotFound();
            
            return View(tblFaculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            tblFaculty tblFaculty = db.tblFaculties.Find(id);
            
            // Instead of actually deleting, we set Active to false.
            tblFaculty.FacultyActive = false;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) db.Dispose();
            
            base.Dispose(disposing);
        }
    }
}
