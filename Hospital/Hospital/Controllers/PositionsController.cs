using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly hospitaldbContext _db;
        public PositionsController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult PositionsTable()
        {
            return View(_db.Positions.ToList());
        }
    }
}
