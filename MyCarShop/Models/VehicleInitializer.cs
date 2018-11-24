using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyCarShop.App_Start;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyCarShop.Models
{
    public class VehicleInitializer : System.Data.Entity.DropCreateDatabaseAlways<VehicleContext>
    {
        protected override void Seed(VehicleContext context)
        {

            var userManager = new ApplicationUserManager(new UserStore<User>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin = new User { Email = "artemgontar16@gmail.com", Year = 1999, UserName = "artemgontar16@gmail.com" };
            string password = "TUTdm83b7L1";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            byte[] imageData = File.ReadAllBytes(@"S:\GSTU\МДиСУБД\Course\MyCarShop\MyCarShop\img\1.jpg"); ;

            // установка массива байтов
            context.CarBrands.Add(new CarBrand { CarBrandName = "BMW", FactoryCountry = "Германия", FoundationDay = new DateTime(1909, 12, 15), Description = "" });
            context.CarBrands.Add(new CarBrand { CarBrandName = "Audi", FactoryCountry = "Германия", FoundationDay = new DateTime(1916, 4, 25), Description = "" });
            context.CarBrands.Add(new CarBrand { CarBrandName = "Chevrolet", FactoryCountry = "США", FoundationDay = new DateTime(1911, 7, 22), Description = "" });

            context.Models.Add(new Model { ModelName = "1 SERIES", MaxSpeed = 220, Release = new DateTime(2004, 2, 25), NumberOfPassenger = 5, Description = "" });
            context.Models.Add(new Model { ModelName = "2 SERIES", MaxSpeed = 230, Release = new DateTime(2004, 6, 20), NumberOfPassenger = 5, Description = "" });
            context.Models.Add(new Model { ModelName = "3 SERIES", MaxSpeed = 240, Release = new DateTime(2005, 4, 15), NumberOfPassenger = 5, Description = "" });

            context.Vehicles.Add(new Vehicle { Image = imageData, ModelId = 1, CarBrandId = 1, Color = "White", CarBodyType = "Хечбек", Transmission = "Передний", Gearbox = "АКПП", Price = 4000.0m, Used = true, Description = "" });
            context.Vehicles.Add(new Vehicle { Image = imageData, ModelId = 2, CarBrandId = 2, Color = "Black", CarBodyType = "Минивэн", Transmission = "Задний", Gearbox = "АКПП", Price = 3000.0m, Used = true, Description = "" });
            context.Vehicles.Add(new Vehicle { Image = imageData, ModelId = 3, CarBrandId = 3, Color = "BlackBaccardi", CarBodyType = "Седан", Transmission = "Передний", Gearbox = "МКПП", Price = 2000.0m, Used = true, Description = "" });

            base.Seed(context);
        }
    }
}