using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class DiseasesHistoryController : Controller
    {
        private readonly hospitaldbContext _db;
        public DiseasesHistoryController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult DiseasesHistoryTable()
        {
            return View(_db.DiseasesHistories.Include(x => x.Patient).Include(x => x.Disease).Include(x => x.Recipe).ToList());
        }
    }
}
