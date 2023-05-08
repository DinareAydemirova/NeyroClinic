
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeyroClinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool IsDeactive { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public DoctorOffice DoctorOffice { get; set; }
       
        public List<Inpatient> Inpatient { get; set; }
        public List<OutPatient> OutPatient { get; set; }
        //public List<Appointment> Appointment { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
