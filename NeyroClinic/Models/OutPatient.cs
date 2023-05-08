using NeyroClinic.Models;
using System;
using System.Collections.Generic;

namespace NeyroClinic.Models
{
    public class OutPatient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public bool IsFemale { get; set; }
        public string BloodGroup { get; set; }
        public string Treatment { get; set; }
        public DateTime Date { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        //public List<Appointment> Appointment { get; set; }
        
    }
}
