using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Speaker.Models;

namespace Project_Speaker.Controllers
{
    public class SpeakerModelsController : Controller
    {
        private SpeakerDbContext db = new SpeakerDbContext();

        public ActionResult Index()
        {
            return View(db.SpeakerModels.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeakerModel speakerModel = db.SpeakerModels.Find(id);
            if (speakerModel == null)
            {
                return HttpNotFound();
            }
            return View(speakerModel);
        }

       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpeakerModelId,ModelName")] SpeakerModel speakerModel)
        {
            if (ModelState.IsValid)
            {
                db.SpeakerModels.Add(speakerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(speakerModel);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeakerModel speakerModel = db.SpeakerModels.Find(id);
            if (speakerModel == null)
            {
                return HttpNotFound();
            }
            return View(speakerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpeakerModelId,ModelName")] SpeakerModel speakerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speakerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(speakerModel);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeakerModel speakerModel = db.SpeakerModels.Find(id);
            if (speakerModel == null)
            {
                return HttpNotFound();
            }
            return View(speakerModel);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpeakerModel speakerModel = db.SpeakerModels.Find(id);
            db.SpeakerModels.Remove(speakerModel);
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
