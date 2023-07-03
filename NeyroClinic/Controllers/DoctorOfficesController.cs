using NeyroClinic.DAL;
using NeyroClinic.Helpers;
using NeyroClinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorOfficesController : Controller
    {
        public readonly AppDbContext _db;
        public DoctorOfficesController(AppDbContext db)

        {
            _db = db;

        }
        #region Index

        public async Task<IActionResult> Index()
        {
            List<DoctorOffice> doctorOffices = await _db.DoctorOffices.Include(x => x.Doctor).ToListAsync();
            return View(doctorOffices);
        }

        #endregion

        #region Update

        #region Get

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DoctorOffice dbDoctorOffice = await _db.DoctorOffices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctorOffice == null)
            {
                return BadRequest();
            }
            return View(dbDoctorOffice);


        }

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(DoctorOffice doctorOffice ,int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DoctorOffice dbDoctorOffice = await _db.DoctorOffices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctorOffice == null)
            {
                return BadRequest();
            }


            bool isExist = await _db.DoctorOffices.AnyAsync(x => x.Name == doctorOffice.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View(dbDoctorOffice);
            }

            dbDoctorOffice.Name= doctorOffice.Name; 
            dbDoctorOffice.Floor= doctorOffice.Floor;
            
         
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #endregion

        #endregion




    }
}
