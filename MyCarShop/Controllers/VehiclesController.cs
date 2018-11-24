using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCarShop.Models;

namespace MyCarShop.Controllers
{
    public class VehiclesController : Controller
    {
        private VehicleContext db = new VehicleContext();

        // GET: Vehicles
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.CarBrand).Include(v => v.Model);
            return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CarBrandId = new SelectList(db.CarBrands, "CarBrandId", "CarBrandName");
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleId,ModelId,CarBrandId,Color,CarBodyType,Transmission,Gearbox,Price,Used,Description")] Vehicle vehicle, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                vehicle.Image = imageData;
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarBrandId = new SelectList(db.CarBrands, "CarBrandId", "CarBrandName", vehicle.CarBrandId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarBrandId = new SelectList(db.CarBrands, "CarBrandId", "CarBrandName", vehicle.CarBrandId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleId,ModelId,CarBrandId,Color,CarBodyType,Transmission,Gearbox,Price,Used,Description")] Vehicle vehicle,HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                vehicle.Image = imageData;
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarBrandId = new SelectList(db.CarBrands, "CarBrandId", "CarBrandName", vehicle.CarBrandId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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
