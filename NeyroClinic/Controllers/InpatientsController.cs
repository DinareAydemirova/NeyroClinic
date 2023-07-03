using NeyroClinic.DAL;
using NeyroClinic.Models;
using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InpatientsController : Controller
    {
        public readonly AppDbContext _db;
        public InpatientsController(AppDbContext db)
        {
            _db= db;

        }

        #region Index

        public async Task<IActionResult> Index()
        {
            List<Inpatient> inpatients = await _db.Inpatients.Include(x=>x.Doctor).Include(x=>x.Ward).ToListAsync();
            return View(inpatients);
        }

        #endregion


        #region Create

        #region Get
        public async Task<IActionResult> Create()
        {
            ViewBag.Wards = await _db.Wards.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            return View();
        }


        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inpatient inpatient, int docId, int wardId)
        {
            ViewBag.Wards= await _db.Wards.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            inpatient.WardId= wardId;
            inpatient.DoctorId = docId;
            await _db.Inpatients.AddAsync(inpatient);



            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion


        #endregion

        #region Update

        #region Get
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Wards = await _db.Wards.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Inpatient inpatient = await _db.Inpatients.FirstOrDefaultAsync(x => x.Id == id);
            if (inpatient == null)
            {
                return BadRequest();
            }
            return View(inpatient);
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Inpatient inpatient, int? id,int docId, int wardId)
        {
            ViewBag.Wards = await _db.Wards.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Inpatient dbInpatient = await _db.Inpatients.FirstOrDefaultAsync(x => x.Id == id);
            if (dbInpatient == null)
            {
                return BadRequest();
            }
            dbInpatient.DoctorId = docId;
            dbInpatient.WardId= wardId;  
            dbInpatient.Name= inpatient.Name;
            dbInpatient.Adress=inpatient.Adress;
            dbInpatient.BirthDate=inpatient.BirthDate;
            dbInpatient.BloodGroup=inpatient.BloodGroup;    
            dbInpatient.DischargedDate=inpatient.DischargedDate;    
            dbInpatient.SurgeryDate=inpatient.SurgeryDate;
            dbInpatient.SurgeryType=inpatient.SurgeryType;
            dbInpatient.IsFemale=inpatient.IsFemale;
            dbInpatient.Phone=inpatient.Phone;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #endregion


        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.Wards = await _db.Wards.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Inpatient inpatient = await _db.Inpatients.FirstOrDefaultAsync(x => x.Id == id);
            if (inpatient == null)
            {
                return BadRequest();
            }
            return View(inpatient);
        }

        #endregion
    }
}
