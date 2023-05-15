using System;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Doctor  Doctor { get; set; }
        public int DoctorId { get; set; }
        [Required]
        public string Patient { get; set; }
        public bool IsDeactive { get; set; }
    }
}
