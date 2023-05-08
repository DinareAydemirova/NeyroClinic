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
    public class EnvironmentalServicesController : Controller
    {
        public readonly AppDbContext _db;
        public EnvironmentalServicesController(AppDbContext db)
        {
            _db = db;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            List<EnvironmentalService> environmentalServices = await _db.EnvironmentalServices.ToListAsync();
            return View(environmentalServices);
        }

        #endregion

        #region Create EnviromentalService

        #region Get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnvironmentalService environmentalService)
        {


            bool isExist = await _db.EnvironmentalServices.AnyAsync(x => x.Name == environmentalService.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.EnvironmentalServices.AddAsync(environmentalService);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        #endregion

        #endregion

        #region Update EnviromentalService

        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EnvironmentalService dbEnviromentalService = await _db.EnvironmentalServices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbEnviromentalService == null)
            {
                return BadRequest();
            }
            return View(dbEnviromentalService);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, EnvironmentalService environmentalService)
        {


            if (id == null)
            {
                return NotFound();
            }
            EnvironmentalService dbEnvironmentalService = await _db.EnvironmentalServices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbEnvironmentalService == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.EnvironmentalServices.AnyAsync(s => s.Name == environmentalService.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbEnvironmentalService.Name = environmentalService.Name;
            dbEnvironmentalService.Adress = environmentalService.Adress;
            dbEnvironmentalService.Age = environmentalService.Age;
            dbEnvironmentalService.StartDate = environmentalService.StartDate;
            dbEnvironmentalService.Number = environmentalService.Number;
            dbEnvironmentalService.Position = environmentalService.Position;
            dbEnvironmentalService.Salary = environmentalService.Salary;


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
            EnvironmentalService environmentalService = await _db.EnvironmentalServices.FirstOrDefaultAsync(x => x.Id == id);
            if (environmentalService == null)
            {
                return BadRequest();
            }
            return View(environmentalService);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EnvironmentalService dbEnvironmentalService = await _db.EnvironmentalServices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbEnvironmentalService == null)
            {
                return BadRequest();
            }
            if (dbEnvironmentalService.IsDeactive)
            {
                dbEnvironmentalService.IsDeactive = false;
            }
            else
            {
                dbEnvironmentalService.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
