using NeyroClinic.DAL;
using NeyroClinic.Models;
using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WardsController : Controller
    {
        public readonly AppDbContext _db;
        public WardsController(AppDbContext db)
        {
            _db = db;

        }

        #region Index

        public async Task<IActionResult> Index()
        {
            List<Ward> wards = await _db.Wards.ToListAsync();

            return View(wards);
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
        public async Task<IActionResult> Create(Ward ward)
        {

            bool isExist = await _db.Wards.AnyAsync(x => x.RoomNumber == ward.RoomNumber);
            if (isExist)
            {
                ModelState.AddModelError("RoomNumber", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Wards.AddAsync(ward);
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
            Ward dbWard = await _db.Wards.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWard == null)
            {
                return BadRequest();
            }
            return View(dbWard);
        }
        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Ward ward,int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ward dbWard = await _db.Wards.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWard == null)
            {
                return BadRequest();
            }

            dbWard.RoomNumber = ward.RoomNumber;
            dbWard.Floor=ward.Floor;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        #endregion



        #endregion


        #region IsDeactive

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ward dbWard = await _db.Wards.FirstOrDefaultAsync(x => x.Id == id);
            if (dbWard == null)
            {
                return BadRequest();
            }
            if (dbWard.IsDeactive)
            {
                dbWard.IsDeactive = false;
            }
            else
            {
                dbWard.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
