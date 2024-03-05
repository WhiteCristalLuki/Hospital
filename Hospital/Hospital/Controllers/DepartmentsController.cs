using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly hospitaldbContext _db;
        public DepartmentsController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult DepartmentsTable()
        {
            return View(_db.Departments.ToList());
        }
    }
}
