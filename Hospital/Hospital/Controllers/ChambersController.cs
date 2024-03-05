using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class ChambersController : Controller
    {
        private readonly hospitaldbContext _db;
        public ChambersController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult ChambersTable()
        {
            return View(_db.Chambers.Include(x => x.Corps).ToList());
        }
    }
}
