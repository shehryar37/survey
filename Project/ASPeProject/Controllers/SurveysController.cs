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
    public class SurveysController : Controller {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: Surveys
        public ActionResult Index() {
            var tblSurveys = db.tblSurveys.Include(t => t.tblUserType);
            return View(db.tblSurveys.Where(x => x.SurveyActive).ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblSurvey tblSurvey = db.tblSurveys.Find(id);

            if (tblSurvey == null) {
                return HttpNotFound();
            }

            return View(tblSurvey);
        }

        // GET: Surveys/Create
        public ActionResult Create() {
            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");

            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyID,SurveyTitle,SurveyDescription,UserTypeID,SurveyConducts,SurveyActive")] tblSurvey tblSurvey, string surveyDueDate) {
            if (ModelState.IsValid) {
                // Setting all values which do not require user input to their default values
                #region
                tblSurvey.SurveyConducts = 0;
                tblSurvey.SurveyActive = true;
                tblSurvey.SurveyReportingDateTime = DateTime.Today.ToShortDateString();

                #endregion

                tblSurvey.SurveyDueDate = surveyDueDate;

                db.tblSurveys.Add(tblSurvey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSurvey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");
            tblSurvey tblSurvey = db.tblSurveys.Find(id);

            if (tblSurvey == null) {
                return HttpNotFound();
            }

            return View(tblSurvey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyID,SurveyTitle,SurveyDescription," +
                    "UserTypeID,SurveyConducts,SurveyReportingDateTime")] 
                    tblSurvey tblSurvey, string surveyDueDate) {
            if (ModelState.IsValid) {
                tblSurvey.SurveyActive = true;
                tblSurvey.SurveyDueDate = surveyDueDate;

                db.Entry(tblSurvey).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");
            return View(tblSurvey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblSurvey tblSurvey = db.tblSurveys.Find(id);

            if (tblSurvey == null) {
                return HttpNotFound();
            }

            return View(tblSurvey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            tblSurvey tblSurvey = db.tblSurveys.Find(id);
            tblSurvey.SurveyActive = false;

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
