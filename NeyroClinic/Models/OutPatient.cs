using NeyroClinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models
{
    public class OutPatient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public bool IsFemale { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        [Required]
        public string Treatment { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        //public List<Appointment> Appointment { get; set; }
        
    }
}
