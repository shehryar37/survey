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
    public class FAQsController : Controller
    {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: FAQs
        public ActionResult Index()
        {   
            return View(db.tblFAQs.Where(x=>x.FAQActive).ToList());
        }

        // GET: FAQs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        // GET: FAQs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FAQID,FAQQuestion,FAQAnswer,FAQDateTime,FAQActive")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                tblFAQ.FAQActive = true;
                tblFAQ.FAQDateTime = DateTime.Now;

                db.tblFAQs.Add(tblFAQ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblFAQ);
        }

        // GET: FAQs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FAQID,Question,Answer,Active")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblFAQ);
        }

        // GET: FAQs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            db.tblFAQs.Remove(tblFAQ);

            tblFAQ.FAQActive = false;

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
