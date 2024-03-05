using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class MedecinesController : Controller
    {
        private readonly hospitaldbContext _db;
        public MedecinesController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult MedecinesTable()
        {
            return View(_db.Medecines.ToList());
        }
    }
}
