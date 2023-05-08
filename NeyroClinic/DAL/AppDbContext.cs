using Microsoft.EntityFrameworkCore;
using NeyroClinic.Models.Staff;
using NeyroClinic.Models;

namespace NeyroClinic.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        public DbSet<DoctorOffice> DoctorOffices { get; set; }
       
        public DbSet<Inpatient> Inpatients { get; set; }
        public DbSet<Labaratory> Labaratories { get; set; }
        public DbSet<OutPatient> OutPatients { get; set; }
        public DbSet<Pharmacy> Pharmacys { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Clerical> Clericals { get; set; }
        public DbSet<EnvironmentalService> EnvironmentalServices { get; set; }
        public DbSet<FoodService> FoodServices { get; set; }
        public DbSet<IT> ITs { get; set; }
        public DbSet<Janitorial> Janitorials { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<PharmacyStaff> PharmacyStaff { get; set; }
        public DbSet<Technician> Technicians { get; set; }

    }
}
