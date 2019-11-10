using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyProject.Controllers {
    public class CompetitionsController : Controller {
        SurveyDBEntities db = new SurveyDBEntities();

        // GET: Competitions
        public ActionResult Index() {
            // return only those competitions which are active.
            return View(db.tblCompetitions.Where(x => x.CompActive).ToList());
        }

        // GET: Competitions/Details
        public ActionResult Details(int? id) {
            // Checking if an ID is present.
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Searching for record with given ID. 
            tblCompetition competition = db.tblCompetitions.Find(id);

            // If record does not exist, return a not found error.
            if (competition == null) return HttpNotFound();

            return View(competition);
        }

        // GET: Competitions/ViewParticipants
        public ActionResult ViewParticipants(int? id) {
            // return only those competitions which are active.
            return View(db.tblCompParticipants.Where(x => x.CompID == id).ToList());
        }

        // GET: Competitions/SelectWinner
        public ActionResult SelectWinner(int? id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.FirstWinner = new SelectList(db.tblCompParticipants.Where(x => x.CompID == id), "CompParticID", "CompParticName");
            ViewBag.SecondWinner = new SelectList(db.tblCompParticipants.Where(x => x.CompID == id), "CompParticID", "CompParticName");
            ViewBag.ThirdWinner = new SelectList(db.tblCompParticipants.Where(x => x.CompID == id), "CompParticID", "CompParticName");

            return View();
        }

        // POST: Competitions/SelectWinner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectWinner([Bind(Include = "CompWinID,FirstWinner,SecondWinner,ThirdWinner")] tblCompWinner winner) {
            tblCompParticipant comp = db.tblCompParticipants.Find(winner.FirstWinner);
            int compID = comp.CompID;
            if (ModelState.IsValid) {
                int count = db.tblCompWinners.Where(t => t.tblCompParticipant.CompID == compID).Count();

                if (count == 0) {
                    if ((winner.FirstWinner != winner.SecondWinner)
                                && (winner.FirstWinner != winner.ThirdWinner)
                                && (winner.SecondWinner != winner.ThirdWinner)) {
                        db.tblCompWinners.Add(winner);
                        db.SaveChanges();

                        // Take user to index right after submission is done.
                        return RedirectToAction("Index");
                    } else {
                        // Show some error that the three dropdowns cannot be the same.
                    }
                } else {
                    // Show some error that there are already winners for this competition
                }
            }

            ViewBag.FirstWinner = new SelectList(db.tblCompParticipants.Where(x => x.CompID == compID), "CompParticID", "CompParticName");
            ViewBag.SecondWinner = new SelectList(db.tblCompParticipants.Where(x => x.CompID == compID), "CompParticID", "CompParticName");
            ViewBag.ThirdWinner = new SelectList(db.tblCompParticipants.Where(x => x.CompID == compID), "CompParticID", "CompParticName");

            return View(winner);
        }

        // GET: Competitions/Create
        public ActionResult Create() {
            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");

            return View();
        }

        // POST: Competitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompID,CompName,CompDescription,UserTypeID")] tblCompetition comp, string CompetitionDate) {
            if (ModelState.IsValid) {
                // Setting Active to true whenever a new Competition is created. It is false only when a competition is deleted.
                comp.CompActive = true;

                // Setting the date on which competition is held.
                comp.CompDate = CompetitionDate;

                db.tblCompetitions.Add(comp);
                db.SaveChanges();

                // Take user to index right after submission is done.
                return RedirectToAction("Index");
            }

            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");
            return View(comp);
        }

        // GET: Competition/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblCompetition competition = db.tblCompetitions.Find(id);

            if (competition == null) return HttpNotFound();

            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");
            return View(competition);
        }

        // POST: Competition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompID,CompName,CompDescription,UserTypeID")] tblCompetition competition, string CompetitionDate) {
            if (ModelState.IsValid) {
                // Setting active property true just to be on the safe side.
                competition.CompActive = true;

                // Setting the date on which competition is held.
                competition.CompDate = CompetitionDate;

                db.Entry(competition).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UserTypeID = new SelectList(db.tblUserTypes, "UserTypeID", "UserTypeName");
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblCompetition competition = db.tblCompetitions.Find(id);
            if (competition == null) return HttpNotFound();

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            tblCompetition competition = db.tblCompetitions.Find(id);

            // Instead of actually deleting Competition, the Active field is set to False.
            // This way, the competition appears deleted, but can be recovered if need be.
            competition.CompActive = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}