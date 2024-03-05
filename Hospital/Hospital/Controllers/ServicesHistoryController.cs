using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class ServicesHistoryController : Controller
    {
        private readonly hospitaldbContext _db;

        public ServicesHistoryController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult ServicesHistoryTable()
        {
            return View(_db.ServicesHistories.Include(x => x.Service).Include(x => x.Patient).ToList());
        }
    }
}
