using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static NeyroClinic.Helpers.Helper;

namespace NeyroClinic.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class OutpatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
