using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly hospitaldbContext _db;
        public ServicesController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult ServicesTable()
        {
            return View(_db.Services.Include(x => x.Corps).ToList());
        }
    }
}
