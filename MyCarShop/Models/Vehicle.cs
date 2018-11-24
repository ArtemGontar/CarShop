using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCarShop.Models
{
    public class Vehicle
    {
        [Required]
        public int VehicleId { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required]
        public int CarBrandId { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string CarBodyType { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public string Gearbox { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool Used { get; set; }
        public string Description { get; set; }
        [ForeignKey("ModelId")]
        public virtual Model Model { get; set; }
        [ForeignKey("CarBrandId")]
        public virtual CarBrand CarBrand { get; set; }
    }
}