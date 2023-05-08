using System;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set;}
        [Required]
        public DateTime ExpireDate { get; set; }
        [Required]
        public int Stock { get; set; }
        
    }
}
