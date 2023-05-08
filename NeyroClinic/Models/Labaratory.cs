using System;

namespace NeyroClinic.Models
{
    public class Labaratory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Resault { get; set; }
        public string Patient { get; set; }

    }
}
