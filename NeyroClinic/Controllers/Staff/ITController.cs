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
    public class ITController : Controller
    {
        public readonly AppDbContext _db;
        public ITController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<IT> It = await _db.ITs.ToListAsync();
            return View(It);
        }
        #endregion

        #region Create ITStaff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IT It)
        {


            bool isExist = await _db.ITs.AnyAsync(x => x.Name == It.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.ITs.AddAsync(It);
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
            IT dbIt = await _db.ITs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbIt == null)
            {
                return BadRequest();
            }
            return View(dbIt);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, IT It)
        {


            if (id == null)
            {
                return NotFound();
            }
            IT dbIt = await _db.ITs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbIt == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.ITs.AnyAsync(s => s.Name == It.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbIt.Name = It.Name;
            dbIt.Adress = It.Adress;
            dbIt.Age = It.Age;
            dbIt.StartDate = It.StartDate;
            dbIt.Number = It.Number;
            dbIt.Position = It.Position;
            dbIt.Salary = It.Salary;


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
            IT It= await _db.ITs.FirstOrDefaultAsync(x => x.Id == id);
            if (It == null)
            {
                return BadRequest();
            }
            return View(It);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IT dbIt = await _db.ITs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbIt == null)
            {
                return BadRequest();
            }
            if (dbIt.IsDeactive)
            {
                dbIt.IsDeactive = false;
            }
            else
            {
                dbIt.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
