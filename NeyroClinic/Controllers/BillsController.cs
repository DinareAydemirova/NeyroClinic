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
    public class BillsController : Controller
    {
        private readonly AppDbContext _db;
        public BillsController(AppDbContext db)
        {
            _db = db;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            List<Bill> bill = await _db.Bills.ToListAsync();
            return View(bill);
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
        public async Task<IActionResult> CreateAsync(Bill bill)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _db.Bills.AddAsync(bill);
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
            Bill dbBill = await _db.Bills.FirstOrDefaultAsync(x => x.Id == id);
            if (dbBill == null)
            {
                return BadRequest();
            }
            return View(dbBill);
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Bill bill, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bill dbBill = await _db.Bills.FirstOrDefaultAsync(x => x.Id == id);
            if (dbBill == null)
            {
                return BadRequest();
            }

            dbBill.DoctorName= bill.DoctorName;
            dbBill.PatientName= bill.PatientName;
            dbBill.Date= bill.Date;
            dbBill.Insurance= bill.Insurance;
            dbBill.Discount= bill.Discount;
            dbBill.Tax= bill.Tax;
            dbBill.Total= bill.Total;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        #endregion


        #endregion






    }
}