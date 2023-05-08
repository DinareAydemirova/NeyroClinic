using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeyroClinic.Models
{
    public class DoctorOffice
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Floor { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

    }
}
