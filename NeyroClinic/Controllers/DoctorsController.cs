using NeyroClinic.Helpers;
using NeyroClinic.DAL;
using NeyroClinic.Models;
using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        public readonly IWebHostEnvironment _env;
        public readonly AppDbContext _db;
        public DoctorsController(AppDbContext db, IWebHostEnvironment env)

        {
            _db = db;
            _env = env;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Doctor> doctors = await _db.Doctors.Include(x=>x.Department).Include(x=>x.DoctorOffice).ToListAsync();
            return View(doctors);
        }
        #endregion

        #region Create

        #region Create get
        public async Task<IActionResult> Create()
        {

            ViewBag.Departments = await _db.Departments.ToListAsync();

            return View();
        }
        #endregion

        #region Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor, int depId)
        {
            ViewBag.Departments = await _db.Departments.ToListAsync();


            bool isExist = await _db.Doctors.AnyAsync(x => x.FullName == doctor.FullName);
            if (isExist)
            {
                ModelState.AddModelError("FullName", "This Name is already exist");
                return View();
            }

            if (doctor.Photo == null)

            {
                ModelState.AddModelError("Photo", "Please select Image");
                return View();
            }
            if (!doctor.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select Image file");
                return View();
            }
            if (doctor.Photo.IsOlder2MB())
            {
                ModelState.AddModelError("Photo", "It must be max 2mb");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath,"assets", "img");
           
            doctor.Image = await doctor.Photo.SaveImageAsync(folder);
            doctor.DepartmentId = depId;
            await _db.Doctors.AddAsync(doctor);


           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        #endregion

        #endregion


        #region Update

        #region Get

        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.Departments = await _db.Departments.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
            return View(dbDoctor);

           
        }

        #endregion

        #region post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Doctor doctor, int depId , int? id)
        {

            ViewBag.Departments = await _db.Departments.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
           

            bool isExist = await _db.Doctors.AnyAsync(x => x.FullName == doctor.FullName&& x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View(dbDoctor);
            }

            if (doctor.Photo != null)
            {
                if (!doctor.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select Image file");
                    return View(dbDoctor);
                }
                if (doctor.Photo.IsOlder2MB())
                {
                    ModelState.AddModelError("Photo", "It must be max 2mb");
                    return View(dbDoctor);
                }

                string folder = Path.Combine(_env.WebRootPath, "assets", "img");
                if (doctor.Image != null)
                {
                    Extension.DeleteFile(folder, doctor.Image);
                }

                dbDoctor.Image = await doctor.Photo.SaveImageAsync(folder);

            }

            dbDoctor.FullName = doctor.FullName;
            dbDoctor.Age = doctor.Age;
            dbDoctor.Phone = doctor.Phone;
            dbDoctor.Email = doctor.Email;
            dbDoctor.Description = doctor.Description;

            dbDoctor.DepartmentId = depId;
           

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        #endregion



        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.Departments = await _db.Departments.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
            return View(dbDoctor);
        }

        #endregion

        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Doctor dbDoctor = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDoctor == null)
            {
                return BadRequest();
            }
            if (dbDoctor.IsDeactive)
            {
                dbDoctor.IsDeactive = false;
            }
            else
            {
                dbDoctor.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
