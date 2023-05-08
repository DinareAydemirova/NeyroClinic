using System;

namespace NeyroClinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Doctor  Doctor { get; set; }
        public int DoctorId { get; set; }
        public OutPatient OutPatient { get; set; }
        public int OutPatientId { get; set; }
        public Inpatient Inpatient { get; set; }
        public int InpatientId { get; set;}
    }
}
