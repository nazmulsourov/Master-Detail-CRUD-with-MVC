using Project_Speaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Speaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpeakerDbContext db=new SpeakerDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Aggrigate()
        {
            int TotalProduct = db.Stocks.Count();
            ViewBag.TotalProduct = TotalProduct;

            int totalProductPrice = (int)db.Stocks.Sum(x => x.Price);
            ViewBag.totalProductPrice = totalProductPrice;

            int MaxPrice = (int)db.Stocks.Max(x => x.Price);
            ViewBag.MaxPrice = MaxPrice;

            int MinPrice = (int)db.Stocks.Min(x => x.Price);
            ViewBag.MinPrice = MinPrice;

            int AvgPrice = (int)db.Stocks.Average(x => x.Price);
            ViewBag.AvgPrice = AvgPrice;
            return View();
        }
    }
}