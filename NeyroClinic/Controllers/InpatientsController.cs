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
    //[Authorize(Roles = "Admin")]
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

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inpatient inpatient, int doctorId, int wardId)
        {
            ViewBag.Wards= await _db.Wards.ToListAsync();
            ViewBag.Doctors = await _db.Doctors.ToListAsync();
            
            
            if (!ModelState.IsValid)
            {
                return View(inpatient);
            }

            inpatient.DoctorId = doctorId;

            inpatient.WardId= wardId;
          
            await _db.Inpatients.AddAsync(inpatient);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        #endregion

        #endregion

        #endregion



        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
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
