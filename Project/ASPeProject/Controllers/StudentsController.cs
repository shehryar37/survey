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
    public class StudentsController : Controller {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: Students
        public ActionResult Index() {
            var tblStudents = db.tblStudents.Include(t => t.tblSection).Include(t => t.tblUsers);
            return View(tblStudents.Where(x => x.StudentActive).ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(string id) {
            // Checking if an ID is present.
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Searching for record with given ID. 
            tblStudent tblStudent = db.tblStudents.Find(id);

            // If record does not exist, return a not found error.
            if (tblStudent == null) return HttpNotFound();

            return View(tblStudent);
        }

        // GET: Students/Create
        public ActionResult Create() {
            ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName");
            ViewBag.SectionID = new SelectList(db.tblSections, "SectionID", "SectionName");
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "UserName");

            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentName,StudentEmail,SectionID,StudentGender")] tblStudent tblStudent) {
            if (ModelState.IsValid) {
                // If sectionID value is 0 (no selection), return without doing anything.
                if (tblStudent.SectionID == 0) {
                    ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName");
                    ViewBag.SectionID = new SelectList(db.tblSections, "SectionID", "SectionName", tblStudent.SectionID);
                    ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "UserName", tblStudent.StudentID);
                    
                    return JavaScript("Error: Select a section!");
                    //return View(tblStudent);
                }
                
                // Setting Active to true whenever a new Student is created. It is false only when a user is deleted.
                tblStudent.StudentActive = true;

                // Setting an ID for new student by finding total number of records and incrementing by one.
                int count = db.tblStudents.Count(); count++;
                tblStudent.StudentID = "student" + count;

                // Setting an admission date that is the exact date student is created.
                tblStudent.StudentAdmissionDate = DateTime.Today.ToShortDateString();

                db.tblStudents.Add(tblStudent);
                db.SaveChanges();

                // Take user to index right after submission is done.
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.tblClasses, "ClassID", "ClassName");
            ViewBag.SectionID = new SelectList(db.tblSections, "SectionID", "SectionName", tblStudent.SectionID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "UserName", tblStudent.StudentID);

            return View(tblStudent);
        }
        
        // Used for updating the section dropdown based on selected class in the class dropdown.
        [HttpPost]
        public ActionResult LoadSectionsByClass(string classID) {
            int id = Int32.Parse(classID);

            List<tblSection> sectionsList = db.tblSections.Where(s => s.ClassID == id).ToList();

            SelectList Sections = new SelectList(sectionsList, "SectionID", "SectionName");
            return Json(Sections);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblStudent tblStudent = db.tblStudents.Find(id);
            if (tblStudent == null) return HttpNotFound();

            ViewBag.SectionID = new SelectList(db.tblSections, "SectionID", "SectionName", tblStudent.SectionID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "UserName", tblStudent.StudentID);

            return View(tblStudent);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,StudentName,StudentEmail,SectionID,StudentGender,StudentAdmissionDate")] tblStudent tblStudent) {
            if (ModelState.IsValid) {
                // Setting active property true just to be on the safe side.
                tblStudent.StudentActive = true;

                db.Entry(tblStudent).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SectionID = new SelectList(db.tblSections, "SectionID", "SectionName", tblStudent.SectionID);
            ViewBag.StudentID = new SelectList(db.tblUsers, "UserID", "UserName", tblStudent.StudentID);

            return View(tblStudent);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            tblStudent tblStudent = db.tblStudents.Find(id);
            if (tblStudent == null) return HttpNotFound();
            
            return View(tblStudent);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            tblStudent tblStudent = db.tblStudents.Find(id);

            // Instead of actually deleting student, the Active field is set to False.
            // This way, the user appears deleted, but can be recovered if need be.
            tblStudent.StudentActive = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
