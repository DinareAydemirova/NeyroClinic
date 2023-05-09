using System;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models
{
    public class Labaratory
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Resault { get; set; }
        [Required]
        public string Patient { get; set; }

    }
}
