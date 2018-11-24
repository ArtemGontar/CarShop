namespace MyCarShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarBrands",
                c => new
                    {
                        CarBrandId = c.Int(nullable: false, identity: true),
                        CarBrandName = c.String(nullable: false),
                        FactoryCountry = c.String(nullable: false),
                        FoundationDay = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CarBrandId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false),
                        MaxSpeed = c.Int(nullable: false),
                        Release = c.DateTime(nullable: false),
                        NumberOfPassenger = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ModelId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        ModelId = c.Int(nullable: false),
                        CarBrandId = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                        CarBodyType = c.String(nullable: false),
                        Transmission = c.String(nullable: false),
                        Gearbox = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Used = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.CarBrands", t => t.CarBrandId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.CarBrandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Vehicles", "CarBrandId", "dbo.CarBrands");
            DropIndex("dbo.Vehicles", new[] { "CarBrandId" });
            DropIndex("dbo.Vehicles", new[] { "ModelId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Models");
            DropTable("dbo.CarBrands");
        }
    }
}
