using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeyroClinic.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
