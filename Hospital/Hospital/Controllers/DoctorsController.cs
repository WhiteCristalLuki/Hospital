using Hospital.Data;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace Hospital.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly hospitaldbContext _db;
        public DoctorsController(hospitaldbContext db) { 
            _db = db;
        }
        public IActionResult DoctorsTable()
        {
            return View(_db.Doctors.Include(p => p.Positions).Include(d => d.Departments).ToList());
        }
        [HttpGet]
        public IActionResult AddDoctor()
        {
            var doctorViewModel = new DoctorAddViewModel { 
                DepartmentsTitles = _db.Departments.Select(d => d.Title).ToList(),
                PositionsTitles = _db.Positions.Select(d => d.Title).ToList()
            };
            return View(doctorViewModel);
        }
        [HttpPost]
        public IActionResult AddDoctor(DoctorAddViewModel model) {

            List<Department> departments = new();
            foreach(var department in model.Departments)
            {
                departments.Add(_db.Departments.First(x => x.Title == department));
            }

            List<Position> positions = new();
            foreach (var position in model.Positions)
            {
                positions.Add(_db.Positions.First(x => x.Title == position));
            }
            Doctor doctor = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Departments = departments,
                Positions = positions
            };

            _db.Doctors.Add(doctor);
            _db.SaveChanges();
            return View("DoctorsTable", _db.Doctors.Include(p => p.Positions).Include(d => d.Departments).ToList());
        }
        [HttpGet]
        public IActionResult UpdateDoctor(int id)
        {
            Doctor? doctor = _db.Doctors.Include(d => d.Departments).Include(p => p.Positions).FirstOrDefault(x => x.Id == id);
            DoctorUpdateViewModel model = new DoctorUpdateViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                MiddleName = doctor.MiddleName,
                Departments = doctor.Departments.Select(d => d.Title).ToList(),
                Positions = doctor.Positions.Select(d => d.Title).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateDoctor(DoctorUpdateViewModel model)
        {
            var doctorToUpdate = _db.Doctors
                .Include(d => d.Positions)
                .Include(d => d.Departments)
                .FirstOrDefault(d => d.Id == model.Id);

            if (doctorToUpdate != null)
            {
                doctorToUpdate.FirstName = model.FirstName;
                doctorToUpdate.LastName = model.LastName;
                doctorToUpdate.MiddleName = model.MiddleName;

                doctorToUpdate.Departments.Clear();
                foreach (var dep in model.Departments)
                {
                    var department = _db.Departments.First(x => x.Title == dep);
                    if (department != null)
                    {
                        doctorToUpdate.Departments.Add(department);
                    }
                }

                // Обновляем должности доктора
                doctorToUpdate.Positions.Clear(); // Удаляем все существующие связи
                foreach (var pos in model.Positions)
                {
                    var position = _db.Positions.First(x => x.Title == pos);
                    if (position != null)
                    {
                        doctorToUpdate.Positions.Add(position);
                    }
                }
                _db.SaveChanges();
            }
            
            return View("DoctorsTable", _db.Doctors.Include(p => p.Positions).Include(d => d.Departments).ToList());
        }

        public IActionResult DeleteDoctor(int id)
        {
            var doctorToDelete = _db.Doctors.Include(d => d.Positions).Include(d => d.Departments).Include(r => r.Recipes)
                .FirstOrDefault(d => d.Id == id);
            if (doctorToDelete != null)
            {
                doctorToDelete.Positions.Clear();
                doctorToDelete.Departments.Clear();
                doctorToDelete.Recipes.Clear();
                _db.Doctors.Remove(doctorToDelete);
                _db.SaveChanges();
            }
            return View("DoctorsTable", _db.Doctors.Include(p => p.Positions).Include(d => d.Departments).ToList());
        }
    }


}
