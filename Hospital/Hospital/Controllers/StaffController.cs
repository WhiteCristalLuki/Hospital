using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly hospitaldbContext _db;
        public StaffController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult StaffTable()
        {
            return View(_db.staff.Include(x => x.Position).ToList());
        }
    }
}
