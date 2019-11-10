using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyProject.Controllers {
    public class FacultyRegReqController : Controller {
        private SurveyDBEntities db = new SurveyDBEntities();
        
        // GET: FacultyRegReq
        public ActionResult Index() {
            // Fetching all active users.
            var tblFacultiesUsers = db.tblUsers.Include(t => t.tblFaculty).Where(x => x.UserActive);

            // Returning only those users which have TypeID = 3 (TypeID 3 is Faculty).
            return View(tblFacultiesUsers.Where(a => a.tblUserType.UserTypeID == 3).ToList());
        }

        // GET: FacultyRegReq/Edit
        public ActionResult Edit(int? id) {
            // Checking if an ID value is present or not.
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // If ID is not null, search for a user with that ID.
            tblUser user = db.tblUsers.Find(id);

            // If no user with given ID is found, give error.
            if (user == null) return HttpNotFound();

            return View(user);
        }

        // POST: FacultyRegReq/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,StaffNo,UserRegStatus,UserRequestDate,UserReqRejectReason")] tblUser user) {
            if (ModelState.IsValid) {
                // Setting active property true just to be on the safe side.
                user.UserActive = true;

                // Setting typeID to 3 (Faculty / Staff)
                user.UserTypeID = 3;

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