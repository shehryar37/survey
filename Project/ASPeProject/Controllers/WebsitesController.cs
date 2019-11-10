using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace SurveyProject.Controllers {
    public class WebsitesController : Controller {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: Website/Index
        public ActionResult Index() {
            return View();
        }

        // GET: Website/Login
        public ActionResult Login() {
            return View();
        }

        // POST: Website/Login
        // The Login button in Login.cshtml
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,UserPassword")] tblUser tblUser) {
            if (ModelState.IsValid) {
                // Fetching the user details if Username and Password match any record in database.
                tblUser user = db.tblUsers.Where(x => x.UserName == tblUser.UserName &&
                                                      x.UserPassword == tblUser.UserPassword).FirstOrDefault();

                if (user == null) return HttpNotFound();
                Session["user"] = user;     // Storing object in Session for use elsewhere.

                // Check if the user is type 4. UserTypeID 4 is Admin.
                if (user.UserTypeID == 4) {
                    // If the user is an admin, redirect to a page in Admin Portal
                    return Redirect("~/Students/Index");
                } else {
                    // Otherwise, send logged in user to website homepage.
                    return Redirect("~/Websites/Index");
                }
            }

            return View();
        }

        // Logout button in the profile dropdown from header in Admin menu.
        // This method can be called from anywhere by having an anchor link href="~/Websites/Logout"
        public ActionResult Logout() {
            // Clearing out the session.
            Session["user"] = null;
            Session.Abandon();
            Session.Clear();

            return Redirect("~/Websites/Login");
        }

        // GET: Website/Register
        public ActionResult Register() {
            // For populating the User Type dropdown.
            // It has the Where clause to avoid fetching UserTypeID 4 which is Admin.
            ViewBag.UserTypeID = new SelectList(db.tblUserTypes.Where(x => x.UserTypeID != 4), "UserTypeID", "UserTypeName");

            return View();
        }

        // POST: Website/Register
        // Register button in Website/Register.cshtml
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserTypeID")] tblUser user, string RollNo, string StaffNo) {
            // Checking if the user is a Student (Type ID 2)
            if (user.UserTypeID == 2) {
                // Checking if a student exists in database.
                int stdcount = db.tblStudents.Where(t => t.StudentID == RollNo).Count();

                // If a student exists, accept submission for account.
                if (stdcount > 0) {
                    // Check if an account of the same student is already registered.
                    int count = db.tblUsers.Where(t => t.StudentNo == RollNo).Count();

                    // If there is no submission or account already present, submit the request.
                    if (count == 0) {
                        // Setting Roll number / Employee number, Active value, Request date, and Registration Status.
                        user.StudentNo = RollNo;
                        user.UserActive = true;
                        user.UserRequestDate = DateTime.Now.ToShortDateString();
                        user.UserRegStatus = "Requested";

                        db.tblUsers.Add(user);
                        db.SaveChanges();
                    } else {
                        // TODO: Show an error, as the student request has already been submitted.
                        //ViewBag.ShowError = "Student Request has already been submitted!";
                    }
                } else {
                    // TODO: Show an error, as the student record does not exist.
                    
                    // The code below kinda works, but it sends the user to another page filled with errors.
                    // We don't want that to happen, therefore there is a need to search for alternatives.
                    //return HttpNotFound("Student record does not exist!");
                    
                }
            } 
            // Checking if the user is Faculty / Staff (Type ID 3) 
            else if (user.UserTypeID == 3) {
                // Checking if a faculty exists in database.
                int Fcount = db.tblFaculties.Where(t => t.FacultyID == StaffNo).Count();

                // If a faculty exists, accept submission for account.
                if (Fcount > 0) {
                    // Check if an account of the same faculty member is already registered.
                    int count = db.tblUsers.Where(t => t.StaffNo == StaffNo).Count();

                    // If there is no submission or account already present, submit the request.
                    if (count == 0) {
                        // Setting Staff No, Active, Submission request date, and Registration Status.
                        user.StaffNo = StaffNo;
                        user.UserActive = true;
                        user.UserRequestDate = DateTime.Now.ToShortDateString();
                        user.UserRegStatus = "Requested";

                        db.tblUsers.Add(user);
                        db.SaveChanges();
                    } else {
                        // TODO: Show an error, as the faculty request has already been submitted.
                        //ViewBag.ShowError = "Staff Request has already been submitted!";
                    }
                } else {
                    // TODO: Show an error, as the faculty record does not exist.
                    //ViewBag.ShowError = "Staff record does not exist!";
                }
            }

            ViewBag.UserTypeID = new SelectList(db.tblUserTypes.Where(x => x.UserTypeID != 4), "UserTypeID", "UserTypeName");
            return View();
        }

        // GET: Website/FAQs
        public ActionResult FAQs() {
            return View(db.tblFAQs.ToList());
        }
    }
}