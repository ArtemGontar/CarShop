using System;
using System.ComponentModel.DataAnnotations;

namespace MyCarShop.Models
{
    public class Model
    {
        [Required]
        public int ModelId { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        public int MaxSpeed { get; set; }
        [Required]
        public DateTime Release { get; set; }
        [Required]
        public int NumberOfPassenger { get; set; }
        public string Description { get; set; }
        

    }
}