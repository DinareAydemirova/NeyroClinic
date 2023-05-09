using NeyroClinic.DAL;
using NeyroClinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Hospital.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class LabaratoryController : Controller
    {
        public readonly AppDbContext _db;
        public LabaratoryController(AppDbContext db)

        {
            _db = db;

        }
        #region Index

         public async Task<IActionResult> Index()
        {
            List<Labaratory> labaratory = await _db.Labaratories.ToListAsync();
            return View(labaratory);
        }
        #endregion


        #region Create

        #region Get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Labaratory labaratory)
        {
           
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Labaratories.AddAsync(labaratory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        #endregion

        #endregion


        #region Update

        #region Get

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Labaratory dbLabaratory = await _db.Labaratories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbLabaratory == null)
            {
                return BadRequest();
            }
            return View(dbLabaratory);
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Labaratory labaratory,int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Labaratory dbLabaratory = await _db.Labaratories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbLabaratory == null)
            {
                return BadRequest();
            }

            dbLabaratory.Resault = labaratory.Resault;
            dbLabaratory.Patient = labaratory.Patient;
            dbLabaratory.Date = labaratory.Date;
            dbLabaratory.Category = labaratory.Category;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #endregion
    }
}
