using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyCarShop.Models;
using System.Data.Entity;

namespace MyCarShop.Controllers
{
    public class VehiController : ApiController
    {
        VehicleContext db = new VehicleContext();
        public IEnumerable<Vehicle> GetVehicles()
        {
            return db.Vehicles.Include(v => v.CarBrand).Include(v => v.Model);
        }

        public Vehicle GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            return vehicle;
        }
        [HttpPost]
        public void CreateVehicle([FromBody]Vehicle vehicle)
        {
            db.Vehicles.Add(vehicle);
            db.SaveChanges();
        }
        [HttpPut]
        public void EditVehicle(int id, [FromBody]Vehicle vehicle)
        {
            if (id == vehicle.VehicleId)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle != null)
            {
                db.Vehicles.Remove(vehicle);
                db.SaveChanges();
            }
        }
    }
}
