using System;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models
{
    public class Bill
    {
        public int Id { get; set; }
        [Required]

        public string DoctorName { get; set; }
        [Required]

        public string PatientName { get; set; }
        [Required]

        public DateTime Date { get; set; }
        [Required]

        public string Insurance { get; set; }
        [Required]

        public double Tax { get; set; }
        [Required]

        public double Discount { get; set; }
        [Required]

        public double Total { get; set; }
        

    }
}
