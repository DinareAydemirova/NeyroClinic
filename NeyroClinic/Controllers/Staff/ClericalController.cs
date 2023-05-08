using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeyroClinic.DAL;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NeyroClinic.Controllers.Staff
{
    //[Authorize(Roles ="Admin")]
   
    public class ClericalController : Controller
    {
        public readonly AppDbContext _db;
        public ClericalController(AppDbContext db)
        {
            _db = db;
        }
       
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Clerical> clericals = await _db.Clericals.ToListAsync();
            return View(clericals);
        }
        #endregion

        #region Create ClericalStaff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clerical clerical)
        {
           

            bool isExist = await _db.Clericals.AnyAsync(x => x.Name == clerical.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if(!ModelState.IsValid) 
            {
                return View();
            }

            await _db.Clericals.AddAsync(clerical);
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
            Clerical dbClerical = await _db.Clericals.FirstOrDefaultAsync(x => x.Id == id);
            if (dbClerical == null)
            {
                return BadRequest();
            }
            return View(dbClerical);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Clerical clerical)
        {

           
            if (id == null)
            {
                return NotFound();
            }
            Clerical dbClerical= await _db.Clericals.FirstOrDefaultAsync(x => x.Id == id);
            if (dbClerical == null)
            {
                return BadRequest();
            }
           

           
 
            dbClerical.Name = clerical.Name;
            dbClerical.Adress = clerical.Adress;
            dbClerical.Age = clerical.Age;
            dbClerical.StartDate = clerical.StartDate;
            dbClerical.Number = clerical.Number;
            dbClerical.Position = clerical.Position;
            dbClerical.Salary = clerical.Salary;


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
            Clerical clerical = await _db.Clericals.FirstOrDefaultAsync(x => x.Id == id);
            if (clerical == null)
            {
                return BadRequest();
            }
            return View(clerical);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Clerical dbClerical = await _db.Clericals.FirstOrDefaultAsync(x => x.Id == id);
            if (dbClerical == null)
            {
                return BadRequest();
            }
            if (dbClerical.IsDeactive)
            {
                dbClerical.IsDeactive = false;
            }
            else
            {
                dbClerical.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion



    }
}
