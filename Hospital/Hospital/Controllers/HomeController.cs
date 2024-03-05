using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly hospitaldbContext _db;
        public HomeController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult Tables()
        {
            return View(_db);
        }
    }
}
