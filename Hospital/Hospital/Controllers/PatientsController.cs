using Hospital.Data;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly hospitaldbContext _db;
        public PatientsController(hospitaldbContext db)
        {
            _db = db;
        }
        public IActionResult PatientsTable()
        {
            return View(_db.Patients.Include(x => x.Chamber).ToList());
        }
        [HttpGet]
        public IActionResult AddPatient()
        {
            return View(new PatientAddViewModel { });
        }
        [HttpPost]
        public IActionResult AddPatient(PatientAddViewModel model)
        {
            Patient patient = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Age = model.Age,
                Sex = model.Sex,
                InsuranceNumber = model.InsuranceNumber,
                PassportSeries = model.PassportSeries,
                PassportNumber = model.PassportNumber
            };
            _db.Patients.Add(patient);
            _db.SaveChanges();
            return View("PatientsTable", _db.Patients.Include(x => x.Chamber).ToList());
        }
        [HttpGet]
        public IActionResult UpdatePatient(int id)
        {
            var patient = _db.Patients.Include(x => x.Chamber).First(x => x.Id == id);
            PatientUpdateViewModel model = new PatientUpdateViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
                Age = patient.Age,
                Sex = patient.Sex,
                InsuranceNumber = patient.InsuranceNumber,
                PassportSeries = patient.PassportSeries,
                PassportNumber = patient.PassportNumber
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdatePatient(PatientUpdateViewModel model)
        {

            var patientToUpdate = _db.Patients.Include(x => x.Chamber).First(x => x.Id == model.Id);
            var chamber = _db.Chambers.FirstOrDefault(x => x.Id == model.ChamberId);
            if (patientToUpdate != null) {
                patientToUpdate.Id = model.Id;
                patientToUpdate.FirstName = model.FirstName;
                patientToUpdate.LastName = model.LastName;
                patientToUpdate.MiddleName = model.MiddleName;
                patientToUpdate.Age = model.Age;
                patientToUpdate.Sex = model.Sex;
                patientToUpdate.InsuranceNumber = model.InsuranceNumber;
                patientToUpdate.PassportSeries = model.PassportSeries;
                patientToUpdate.PassportNumber = model.PassportNumber;
                if (patientToUpdate.Chamber?.Id != null)
                {
                    if (patientToUpdate.Chamber.Id != model.ChamberId)
                    {
                        patientToUpdate.Chamber.Availability += 1;
                        patientToUpdate.Chamber = chamber;
                        //patientToUpdate.Chamber.Availability -= 1;
                    }
                } else
                {
                    if (model.ChamberId != null)
                    {
                        patientToUpdate.Chamber = chamber;
                        //patientToUpdate.Chamber.Availability -= 1;
                    }              
                }
                _db.SaveChanges();
                return RedirectToAction("PatientsTable", _db.Patients.Include(x => x.Chamber).ToList());
            }

            return View(model);
        }
        public IActionResult DeletePatient(int id)
        {
            var patientToDelete = _db.Patients.Include(x => x.Chamber).First(x => x.Id == id);
            if (patientToDelete != null) {
                if (patientToDelete.Chamber != null)
                {
                    var chamber = _db.Chambers.First(x => x.Id == patientToDelete.Chamber.Id);
                    chamber.Availability += 1;
                }
                var recipes = _db.Recipes.Include(x => x.Patient).Where(x => x.PatientId == patientToDelete.Id).ToList();
                foreach (var recipe in recipes) recipe.PatientId = null;            
                var dhs = _db.DiseasesHistories.Include(x => x.Patient).Where(x => x.PatientId == patientToDelete.Id).ToList();
                foreach (var dh in dhs) dh.PatientId = null;
                var services = _db.ServicesHistories.Include(x => x.Patient).Where(x => x.PatientId == patientToDelete.Id).ToList();
                foreach (var service in services) service.PatientId = null;

                patientToDelete.Recipes.Clear();
                patientToDelete.DiseasesHistories.Clear();
                _db.Patients.Remove(patientToDelete);
                _db.SaveChanges();
            }
            
            return RedirectToAction("PatientsTable", _db.Patients.Include(x => x.Chamber).ToList());
        }
        [HttpGet]
        public IActionResult AddServicesHistory(int id)
        {
            
            var patient = _db.Patients.Find(id);
            var model = new PatientAddServicesHistoryViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddServicesHistory(PatientAddServicesHistoryViewModel model)
        {
            var serviceHistory = new ServicesHistory
            {
                PatientId = model.Id,
                ServiceId = _db.Services.First(x => x.Title == model.Service).Id,
                Date = DateTime.Now,
            };
            _db.ServicesHistories.Add(serviceHistory);
            _db.SaveChanges();
            return RedirectToAction("ServicesHistoryTable", "ServicesHistory", _db.ServicesHistories.Include(x => x.Service).Include(x => x.Patient).ToList());
        }
        [HttpGet]
        public IActionResult AddRecipe(int id)
        {
            var patient = _db.Patients.Find(id);
            var model = new PatientAddRecipeViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddRecipe(PatientAddRecipeViewModel model)
        {
            string[] doctorsFullName = model.DoctorsFullName.Split(" ");
            var patient = _db.Patients.Find(model.Id);
            var doctor = _db.Doctors.First(x => x.FirstName == doctorsFullName[0] &&
                x.LastName == doctorsFullName[1] &&
                x.MiddleName == doctorsFullName[2]);
            var medecine = _db.Medecines.First(x => x.Title == model.MedecineTitle);
            //medecine.Quantity -= 1;
            Recipe recipe = new()
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                MedecineId = medecine.Id,
                PrescribeDate = DateTime.Now
            };
            _db.Recipes.Add(recipe);
            _db.SaveChanges();
            return RedirectToAction("RecipesTable", "Recipes", _db.Recipes.Include(x => x.Patient).Include(x => x.Doctor)
                .Include(x => x.Medecine).ToList());
        }
        [HttpGet]
        public IActionResult AddDiseasesHistory(int id)
        {
            var patient = _db.Patients.Find(id);
            var model = new PatientAddDiseasesHistoryViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddDiseasesHistory(PatientAddDiseasesHistoryViewModel model)
        {
            var diseas = _db.Diseases.First(x => x.Title == model.DiseaseTitle);
            var recipe = _db.Recipes.Find(model.RecipeId);
            var patient = _db.Patients.Find(model.Id);
            DiseasesHistory diseasesHistory = new()
            {
                PatientId = model.Id,
                DiseaseDate = DateTime.Now,
                DiseaseId = diseas.Id,
                Notes = model.Notes,
                RecipeId = recipe.Id
            };
            _db.DiseasesHistories.Add(diseasesHistory);
            _db.SaveChanges();
            return RedirectToAction("DiseasesHistoryTable", "DiseasesHistory", _db.DiseasesHistories.Include(x => x.Patient)
                .Include(x => x.Disease).Include(x => x.Recipe).ToList());
        }
    }
}
