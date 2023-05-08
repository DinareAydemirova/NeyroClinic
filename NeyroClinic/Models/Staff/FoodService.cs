using System;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models.Staff
{
    public class FoodService
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public double Salary { get; set; }
       
        public bool IsDeactive { get; set; }
    }
}
