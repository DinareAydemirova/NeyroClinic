using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeyroClinic.DAL;
using NeyroClinic.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace NeyroClinic.Controllers
{
    public class AppointmentController : Controller
    {
        public readonly AppDbContext _db;
        public AppointmentController(AppDbContext db)

        {
            _db = db;

        }
        #region Index

        public async Task<IActionResult> Index()
        {
            List<Appointment> appointments = await _db.Appointments.Include(x=>x.Doctor).ToListAsync();
            return View(appointments);
        }

        #endregion

        #region Create

        #region Get

        public async Task<IActionResult> Create()
        {
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment, int docId)
        {
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            appointment.DoctorId = docId;
            appointment.Doctor = await _db.Doctors.FindAsync(docId);
            await _db.Appointments.AddAsync(appointment);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #endregion


        #region Update

        #region Get
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Appointment dbAppointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAppointment == null)
            {
                return BadRequest();
            }
            return View(dbAppointment);
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Appointment appointment ,int? id, int docId)
        {
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Appointment dbAppointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAppointment == null)
            {
                return BadRequest();
            }

            dbAppointment.Patient = appointment.Patient;
            dbAppointment.Date = appointment.Date;
            dbAppointment.DoctorId = docId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion


        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appointment dbAppointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAppointment == null)
            {
                return BadRequest();
            }
            if (dbAppointment.IsDeactive)
            {
                dbAppointment.IsDeactive = false;
            }
            else
            {
                dbAppointment.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
