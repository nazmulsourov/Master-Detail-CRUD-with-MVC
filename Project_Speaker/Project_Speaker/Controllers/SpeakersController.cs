using Microsoft.Ajax.Utilities;
using PagedList;

using Project_Speaker.Models;
using Project_Speaker.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Speaker.Controllers
{
    [Authorize]
    public class SpeakersController : Controller
    {
        private readonly SpeakerDbContext db = new SpeakerDbContext();
        // GET: Speakers
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult SpeakerDetails(int pg=1)
        {
            var data = db.Speakers
                        .Include(x => x.Stocks)
                        .Include(x => x.SpeakerModel)
                        .Include(x => x.Brand)
                        .OrderBy(x => x.SpeakerId)
                        .ToPagedList(pg, 5);
            return PartialView("_SpeakerDetails", data);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateForm()
        {
            SpeakerInputModel model = new SpeakerInputModel();
            model.Stocks.Add(new Stock());
            ViewBag.SpeakerModels= db.SpeakerModels.ToList();
            ViewBag.Brands=db.Brands.ToList();
            return PartialView("_CreateForm",model);
        }
        [HttpPost]
        public ActionResult Create(SpeakerInputModel model,string act = "")
        {
            if(act == "add")
            {
                model.Stocks.Add(new Stock());
                foreach(var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Stocks.RemoveAt(index);
                foreach(var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if(act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var speaker = new Speaker
                    {
                        BrandId= model.BrandId,
                        SpeakerModelId= model.SpeakerModelId,
                        Name = model.Name,
                        Realese= model.Realese,
                        Purchasable= model.Purchasable,
                    };
                    //image
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savepath= Path.Combine(Server.MapPath("~/Images"),f);
                    model.Picture.SaveAs(savepath);
                    speaker.Picture = f;

                    db.Speakers.Add(speaker);
                    db.SaveChanges();
                    //stock
                    foreach(var s in model.Stocks)
                    {
                        db.Database.ExecuteSqlCommand($"spInsertStock {(int)s.Category},{s.Price},{(int)s.Quantity},{speaker.SpeakerId}");
                    }
                    SpeakerInputModel newmodel = new SpeakerInputModel()
                    {
                        Name = "",
                        Realese = DateTime.Today
                    };
                    newmodel.Stocks.Add(new Stock());
                    ViewBag.SpeakerModels = db.SpeakerModels.ToList();
                    ViewBag.Brands = db.Brands.ToList();
                    foreach(var e in ModelState.Values)
                    {
                        e.Value = null;
                    }
                    return View("_CreateForm",newmodel);
                }
            }
            ViewBag.SpeakerModels = db.SpeakerModels.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View("_CreateForm", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult EditForm(int id)
        {
            var data = db.Speakers.FirstOrDefault(x => x.SpeakerId == id);
            if (data == null) return new HttpNotFoundResult();
            db.Entry(data).Collection(x => x.Stocks).Load();
            SpeakerEditModel model = new SpeakerEditModel
            {
                SpeakerId = id,
                BrandId = data.BrandId,
                SpeakerModelId = data.SpeakerModelId,
                Name = data.Name,
                Realese = data.Realese,
                Purchasable = data.Purchasable,
                Stocks = data.Stocks.ToList()
            };
            ViewBag.Speaker = db.SpeakerModels.ToList();
            ViewBag.Brand = db.Brands.ToList();
            ViewBag.CurrentPic = data.Picture;
            return PartialView("_EditForm", model);
        }
        [HttpPost]
        public ActionResult Edit(SpeakerEditModel model, string act = "")
        {
            if (act == "add")
            {
                model.Stocks.Add(new Stock());
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Stocks.RemoveAt(index);
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var speker = db.Speakers.FirstOrDefault(x => x.SpeakerId == model.SpeakerId);
                    if (speker == null) return new HttpNotFoundResult();
                    speker.Name = model.Name;
                    speker.Realese = model.Realese;
                    speker.Purchasable = model.Purchasable;
                    speker.SpeakerModelId = model.SpeakerModelId;
                    speker.BrandId = model.BrandId;
                    if (model.Picture != null)
                    {
                        string ext = Path.GetExtension(model.Picture.FileName);
                        string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Path.Combine(Server.MapPath("~/Images"), f);
                        model.Picture.SaveAs(savePath);
                        speker.Picture = f;
                    }
                    else
                    {

                    }
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand($"EXEC DeleteStocks {speker.SpeakerId}");
                    foreach (var e in model.Stocks)
                    {
                        db.Database.ExecuteSqlCommand($" EXEC spInsertStock {(int)e.Category},{(int)e.Price},{(int)e.Quantity},{speker.SpeakerId}");
                    }                 
                }
            }
            ViewBag.Speaker = db.SpeakerModels.ToList();
            ViewBag.Brand = db.Brands.ToList();
            ViewBag.CurrentPic = db.Speakers.FirstOrDefault(x => x.SpeakerId == model.SpeakerId)?.Picture;
            return View("_EditForm", model);
        }
        public ActionResult Delete(int? id)
        {
            var speaker = db.Speakers.Find(id);
            if (speaker != null)
            {
                var deleteStock = db.Stocks.Where(x => x.SpeakerId == id).ToList();
                db.Stocks.RemoveRange(deleteStock);
                db.Speakers.Remove(speaker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}