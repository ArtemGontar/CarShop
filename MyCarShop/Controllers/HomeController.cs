using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCarShop.Models;

namespace MyCarShop.Controllers
{
    public class HomeController : Controller
    {
        private VehicleContext db = new VehicleContext();

        // GET: Home
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.CarBrand).Include(v => v.Model);
            return View(vehicles.ToList()); 
        }

        // GET: Home/Details/5
        

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
