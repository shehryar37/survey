using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyProject.Controllers {
    public class StudentRegReqController : Controller {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: StudentRegReq
        public ActionResult Index() {
            // Fetching all active users.
            var tblStudentUsers = db.tblUsers.Include(t => t.tblStudent).Where(x => x.UserActive);

            // Returning only those users which have TypeID = 2 (TypeID 2 is student).
            return View(tblStudentUsers.Where(a => a.tblUserType.UserTypeID == 2).ToList());
        }

        // GET: StudentRegReq/Edit
        public ActionResult Edit(int? id) {
            // Checking if an ID value is present or not.
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // If ID is not null, search for a user with that ID.
            tblUser user = db.tblUsers.Find(id);

            // If no user with given ID is found, give error.
            if (user == null) return HttpNotFound();

            return View(user);
        }

        // POST: StudentRegReq/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,StudentNo,UserRegStatus,UserRequestDate,UserReqRejectReason")] tblUser user) {
            if (ModelState.IsValid) {
                // Setting active property true.
                user.UserActive = true;

                // Setting typeID to 2 (Student)
                user.UserTypeID = 2;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: StudentRegReq/Delete
        public ActionResult Delete(int? id) {
            // Checking if an ID value is present or not.
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // If ID is not null, search for a user with that ID.
            tblUser user = db.tblUsers.Find(id);

            // If no user with given ID is found, give error.
            if (user == null) return HttpNotFound();

            return View(user);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id) {
            tblUser user = db.tblUsers.Find(id);

            // Instead of actually deleting user, the Active field is set to False.
            // This way, the user appears deleted, but can be recovered if need be.
            user.UserActive = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Students/Details
        public ActionResult Details(int? id) {
            // Checking if an ID is present.
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Searching for record with given ID. 
            tblUser user = db.tblUsers.Find(id);

            // If record does not exist, return a not found error.
            if (user == null) return HttpNotFound();

            return View(user);
        }
    }
}