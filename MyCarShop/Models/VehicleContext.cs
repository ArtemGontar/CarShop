using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace MyCarShop.Models
{
    public class VehicleContext : IdentityDbContext<User>
    {
        public VehicleContext() : base("DbConnection")
        {
        }
        public static VehicleContext Create()
        {
            return new VehicleContext();
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}