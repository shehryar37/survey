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
    public class SupportsController : Controller
    {
        private SurveyDBEntities db = new SurveyDBEntities();

        // GET: Supports
        public ActionResult Index()
        {
            return View(db.tblSupports.Where(x=>x.SupportActive).ToList());
        }

        // GET: Supports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSupport tblSupport = db.tblSupports.Find(id);
            if (tblSupport == null)
            {
                return HttpNotFound();
            }
            return View(tblSupport);
        }

        // GET: Supports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupportID,SupportNumber,SupportEmail,SupportAddress,SupportPerson,ReportingDateTime,SupportActive")] tblSupport tblSupport)
        {
            if (ModelState.IsValid)
            {
                tblSupport.SupportActive = true;

                tblSupport.ReportingDateTime = DateTime.Now;
                db.tblSupports.Add(tblSupport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSupport);
        }

        // GET: Supports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblSupport tblSupport = db.tblSupports.Find(id);

            if (tblSupport == null)
            {
                return HttpNotFound();
            }

            return View(tblSupport);
        }

        // POST: Supports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupportID,SupportNumber,SupportEmail,SupportAddress,SupportPerson,ReportingDateTime,SupportActive")] tblSupport tblSupport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSupport).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tblSupport);
        }

        // GET: Supports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSupport tblSupport = db.tblSupports.Find(id);
            if (tblSupport == null)
            {
                return HttpNotFound();
            }
            return View(tblSupport);
        }

        // POST: Supports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            

            tblSupport tblSupport = db.tblSupports.Find(id);
            db.tblSupports.Remove(tblSupport);

            tblSupport.SupportActive = false;

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
