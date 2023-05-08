using NeyroClinic.DAL;
using NeyroClinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NeyroClinic.Helpers.Helper;

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
        public async Task<IActionResult> Index()
        {
            List<Labaratory> labaratory = await _db.Labaratories.ToListAsync();
            return View(labaratory);
        }
    }
}
