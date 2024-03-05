using Hospital.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly hospitaldbContext _db;
        public RecipesController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult RecipesTable()
        {
            return View(_db.Recipes.Include(x => x.Doctor).Include(x => x.Medecine).Include(x => x.Patient).ToList());
        }
    }
}
