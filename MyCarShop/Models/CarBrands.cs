using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarShop.Models
{
    public class CarBrand
    {
        [Required]
        public int CarBrandId { get; set; }
        [Required]
        public string CarBrandName { get; set; }
        [Required]
        public string FactoryCountry { get; set; }
        [Required]
        public DateTime FoundationDay { get; set; }
        public string Description { get; set; }

    }
}