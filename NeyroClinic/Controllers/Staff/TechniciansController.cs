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
    public class TechniciansController : Controller
    {
        public readonly AppDbContext _db;
        public TechniciansController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Technician> technicians = await _db.Technicians.ToListAsync();
            return View(technicians);
        }
        #endregion

        #region Create TechniciansStaff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Technician technician)
        {


            bool isExist = await _db.Technicians.AnyAsync(x => x.Name == technician.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Technicians.AddAsync(technician);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion



        #endregion


        #region Update ClericalStaff

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Technician dbTechnician = await _db.Technicians.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTechnician == null)
            {
                return BadRequest();
            }
            return View(dbTechnician);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Technician technician)
        {


            if (id == null)
            {
                return NotFound();
            }
            Technician dbTechnician = await _db.Technicians.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTechnician == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.Technicians.AnyAsync(s => s.Name == technician.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbTechnician.Name = technician.Name;
            dbTechnician.Adress = technician.Adress;
            dbTechnician.Age = technician.Age;
            dbTechnician.StartDate = technician.StartDate;
            dbTechnician.Number = technician.Number;
            dbTechnician.Position = technician.Position;
            dbTechnician.Salary = technician.Salary;


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
            Technician technician = await _db.Technicians.FirstOrDefaultAsync(x => x.Id == id);
            if (technician == null)
            {
                return BadRequest();
            }
            return View(technician);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Technician dbTechnician = await _db.Technicians.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTechnician == null)
            {
                return BadRequest();
            }
            if (dbTechnician.IsDeactive)
            {
                dbTechnician.IsDeactive = false;
            }
            else
            {
                dbTechnician.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
