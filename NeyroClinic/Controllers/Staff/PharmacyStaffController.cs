using NeyroClinic.DAL;
using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers.Staff
{
    //[Authorize(Roles = "Admin")]
    public class PharmacyStaffController : Controller
    {
        public readonly AppDbContext _db;
        public PharmacyStaffController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<PharmacyStaff> pharmacyStaff = await _db.PharmacyStaff.ToListAsync();
            return View(pharmacyStaff);
        }
        #endregion

        #region Create PharmacyStaff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PharmacyStaff pharmacyStaff)
        {


            bool isExist = await _db.PharmacyStaff.AnyAsync(x => x.Name == pharmacyStaff.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.PharmacyStaff.AddAsync(pharmacyStaff);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion



        #endregion


        #region Update PharmacyStaff

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PharmacyStaff dbPharmacyStaff = await _db.PharmacyStaff.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPharmacyStaff == null)
            {
                return BadRequest();
            }
            return View(dbPharmacyStaff);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, PharmacyStaff pharmacyStaff)
        {


            if (id == null)
            {
                return NotFound();
            }
            PharmacyStaff dbPharmacyStaff = await _db.PharmacyStaff.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPharmacyStaff == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.PharmacyStaff.AnyAsync(s => s.Name == pharmacyStaff.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbPharmacyStaff.Name = pharmacyStaff.Name;
            dbPharmacyStaff.Adress = pharmacyStaff.Adress;
            dbPharmacyStaff.Age = pharmacyStaff.Age;
            dbPharmacyStaff.StartDate = pharmacyStaff.StartDate;
            dbPharmacyStaff.Number = pharmacyStaff.Number;
            dbPharmacyStaff.Position = pharmacyStaff.Position;
            dbPharmacyStaff.Salary = pharmacyStaff.Salary;


            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion




        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PharmacyStaff pharmacyStaff = await _db.PharmacyStaff.FirstOrDefaultAsync(x => x.Id == id);
            if (pharmacyStaff == null)
            {
                return BadRequest();
            }
            return View(pharmacyStaff);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PharmacyStaff dbPharmacyStaff = await _db.PharmacyStaff.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPharmacyStaff == null)
            {
                return BadRequest();
            }
            if (dbPharmacyStaff.IsDeactive)
            {
                dbPharmacyStaff.IsDeactive = false;
            }
            else
            {
                dbPharmacyStaff.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
