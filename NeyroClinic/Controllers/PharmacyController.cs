using NeyroClinic.DAL;
using NeyroClinic.Models;
using NeyroClinic.Models.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

namespace Hospital.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class PharmacyController : Controller
    {
        public readonly AppDbContext _db;
        public PharmacyController(AppDbContext db)
        {
            _db = db;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            List<Pharmacy> pharmacy = await _db.Pharmacys.ToListAsync();
            return View(pharmacy);
        }

        #endregion

        #region Pharmacy Create

        #region Get

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pharmacy pharmacy)
        {
            bool isExist = await _db.Pharmacys.AnyAsync(x => x.Name == pharmacy.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Name is already exist");
                return View();

            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Pharmacys.AddAsync(pharmacy);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion



        #endregion

        #region Update Pharmacy


        #region get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pharmacy dbPharmacy = await _db.Pharmacys.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPharmacy == null)
            {
                return BadRequest();
            }
            return View(dbPharmacy);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Pharmacy pharmacy)
        {


            if (id == null)
            {
                return NotFound();
            }
            Pharmacy dbPharmacy = await _db.Pharmacys.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPharmacy == null)
            {
                return BadRequest();
            }

            dbPharmacy.Name = pharmacy.Name;
            dbPharmacy.CompanyName = pharmacy.CompanyName;
            dbPharmacy.PurchaseDate = pharmacy.PurchaseDate;
            dbPharmacy.ExpireDate = pharmacy.ExpireDate;
            dbPharmacy.Stock = pharmacy.Stock;
            dbPharmacy.Type = pharmacy.Type;


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
            Pharmacy dbPharmacy = await _db.Pharmacys.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPharmacy == null)
            {
                return BadRequest();
            }
            return View(dbPharmacy);
        }

        #endregion

       

    }
}
