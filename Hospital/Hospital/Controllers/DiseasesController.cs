using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class DiseasesController : Controller
    {
        private readonly hospitaldbContext _db;
        public DiseasesController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult DiseasesTable()
        {
            return View(_db.Diseases.ToList());
        }
    }
}
