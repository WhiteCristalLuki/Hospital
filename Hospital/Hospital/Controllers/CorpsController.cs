using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class CorpsController : Controller
    {
        private readonly hospitaldbContext _db;
        public CorpsController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult CorpsTable()
        {
            return View(_db.Corps.ToList());
        }
    }
}
