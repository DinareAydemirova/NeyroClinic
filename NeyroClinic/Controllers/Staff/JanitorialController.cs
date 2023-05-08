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
    public class JanitorialController : Controller
    {
        public readonly AppDbContext _db;
        public JanitorialController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Janitorial> janitorials = await _db.Janitorials.ToListAsync();
            return View(janitorials);
        }
        #endregion

        #region Create Janitorial Staff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Janitorial janitorial)
        {


            bool isExist = await _db.Janitorials.AnyAsync(x => x.Name == janitorial.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Janitorials.AddAsync(janitorial);
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
            Janitorial dbJanitorial = await _db.Janitorials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbJanitorial == null)
            {
                return BadRequest();
            }
            return View(dbJanitorial);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Janitorial janitorial)
        {


            if (id == null)
            {
                return NotFound();
            }
            Janitorial dbJanitorial = await _db.Janitorials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbJanitorial == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.Janitorials.AnyAsync(s => s.Name == janitorial.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbJanitorial.Name = janitorial.Name;
            dbJanitorial.Adress = janitorial.Adress;
            dbJanitorial.Age = janitorial.Age;
            dbJanitorial.StartDate = janitorial.StartDate;
            dbJanitorial.Number = janitorial.Number;
            dbJanitorial.Position = janitorial.Position;
            dbJanitorial.Salary = janitorial.Salary;


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
            Janitorial janitorial = await _db.Janitorials.FirstOrDefaultAsync(x => x.Id == id);
            if (janitorial == null)
            {
                return BadRequest();
            }
            return View(janitorial);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Janitorial dbJanitorial = await _db.Janitorials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbJanitorial == null)
            {
                return BadRequest();
            }
            if (dbJanitorial.IsDeactive)
            {
                dbJanitorial.IsDeactive = false;
            }
            else
            {
                dbJanitorial.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
