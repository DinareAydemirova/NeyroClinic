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
    [Authorize(Roles = "Admin")]
    public class FoodServicesController : Controller
    {
        public readonly AppDbContext _db;
        public FoodServicesController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<FoodService> foodServices = await _db.FoodServices.ToListAsync();
            return View(foodServices);
        }
        #endregion

        #region Create FoodService Staff

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodService foodService)
        {


            bool isExist = await _db.FoodServices.AnyAsync(x => x.Name == foodService.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.FoodServices.AddAsync(foodService);
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
            FoodService dbFoodService = await _db.FoodServices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbFoodService == null)
            {
                return BadRequest();
            }
            return View(dbFoodService);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, FoodService foodService)
        {


            if (id == null)
            {
                return NotFound();
            }
            FoodService dbFoodService = await _db.FoodServices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbFoodService == null)
            {
                return BadRequest();
            }



            bool isExist = await _db.FoodServices.AnyAsync(s => s.Name == foodService.Name && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();
            }
            dbFoodService.Name = foodService.Name;
            dbFoodService.Adress = foodService.Adress;
            dbFoodService.Age = foodService.Age;
            dbFoodService.StartDate = foodService.StartDate;
            dbFoodService.Number = foodService.Number;
            dbFoodService.Position = foodService.Position;
            dbFoodService.Salary = foodService.Salary;


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
            FoodService foodService = await _db.FoodServices.FirstOrDefaultAsync(x => x.Id == id);
            if (foodService == null)
            {
                return BadRequest();
            }
            return View(foodService);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FoodService dbFoodService = await _db.FoodServices.FirstOrDefaultAsync(x => x.Id == id);
            if (dbFoodService == null)
            {
                return BadRequest();
            }
            if (dbFoodService.IsDeactive)
            {
                dbFoodService.IsDeactive = false;
            }
            else
            {
                dbFoodService.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
