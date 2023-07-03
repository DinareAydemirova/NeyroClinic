using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeyroClinic.DAL;
using NeyroClinic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OutpatientsController : Controller
    {
        public readonly AppDbContext _db;
        public OutpatientsController(AppDbContext db)

        {
            _db = db;

        }
        #region Index

        public async Task<IActionResult> Index()
        {
            List<OutPatient> outpatient = await _db.OutPatients.Include(x=>x.Doctor).ToListAsync();
            return View(outpatient);
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
        public async Task<IActionResult> Create(OutPatient outPatient, int docId)
        {
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            outPatient.DoctorId = docId;
            await _db.OutPatients.AddAsync(outPatient);



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
            OutPatient dbOutPatient = await _db.OutPatients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOutPatient == null)
            {
                return BadRequest();
            }
            return View(dbOutPatient);


        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OutPatient outPatient,int? id, int docId)
        {

            ViewBag.Doctors = await _db.Doctors.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            OutPatient dbOutPatient = await _db.OutPatients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOutPatient == null)
            {
                return BadRequest();
            }

            dbOutPatient.Name = outPatient.Name;
            dbOutPatient.Age = outPatient.Age;
            dbOutPatient.Phone = outPatient.Phone;
            dbOutPatient.Date = outPatient.Date;
            dbOutPatient.Treatment = outPatient.Treatment;
            dbOutPatient.IsFemale = outPatient.IsFemale;

            dbOutPatient.DoctorId = docId;


            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }
        #endregion


        #endregion


        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.Doctors = await _db.Doctors.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            OutPatient dbOutPatient = await _db.OutPatients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbOutPatient == null)
            {
                return BadRequest();
            }
            return View(dbOutPatient);
        }

        #endregion
    }
}
