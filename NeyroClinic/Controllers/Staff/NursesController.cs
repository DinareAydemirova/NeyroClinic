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
    public class NursesController : Controller
    {
        public readonly AppDbContext _db;
        public NursesController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Nurse> nurses = await _db.Nurses.ToListAsync();
            return View(nurses);
        }
        #endregion

        #region Create NursesStaff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nurse nurse)
        {


            bool isExist = await _db.Nurses.AnyAsync(x => x.Name == nurse.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Nurses.AddAsync(nurse);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion



        #endregion


        #region Update NursesStaff

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Nurse dbNurses = await _db.Nurses.FirstOrDefaultAsync(x => x.Id == id);
            if (dbNurses == null)
            {
                return BadRequest();
            }
            return View(dbNurses);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Nurse nurse)
        {


            if (id == null)
            {
                return NotFound();
            }
            Nurse dbNurses = await _db.Nurses.FirstOrDefaultAsync(x => x.Id == id);
            if (dbNurses == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.Nurses.AnyAsync(s => s.Name == nurse.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbNurses.Name = nurse.Name;
            dbNurses.Adress = nurse.Adress;
            dbNurses.Age = nurse.Age;
            dbNurses.StartDate = nurse.StartDate;
            dbNurses.Number = nurse.Number;
            dbNurses.Salary = nurse.Salary;


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
            Nurse nurse = await _db.Nurses.FirstOrDefaultAsync(x => x.Id == id);
            if (nurse == null)
            {
                return BadRequest();
            }
            return View(nurse);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Nurse dbNurse = await _db.Nurses.FirstOrDefaultAsync(x => x.Id == id);
            if (dbNurse == null)
            {
                return BadRequest();
            }
            if (dbNurse.IsDeactive)
            {
                dbNurse.IsDeactive = false;
            }
            else
            {
                dbNurse.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
