using NeyroClinic.DAL;
using NeyroClinic.Models;
using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        public readonly AppDbContext _db;
        public DepartmentsController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Department> departments = await _db.Departments.ToListAsync();
            return View(departments);
        }
        #endregion


        #region Create Department

        #region get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {


            bool isExist = await _db.Departments.AnyAsync(x => x.Name == department.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Departmet Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Departments.AddAsync(department);
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
            Department dbDepartment = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDepartment == null)
            {
                return BadRequest();
            }
            return View(dbDepartment);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Department department)
        {


            if (id == null)
            {
                return NotFound();
            }
            Department dbDepartment = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDepartment == null)
            {
                return BadRequest();
            }




            dbDepartment.Name = department.Name;
           


            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion




        #endregion
    }
}
