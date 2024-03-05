namespace Hospital.Models.ViewModels
{
    public class PatientAddRecipeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string? DoctorsFullName { get; set; }
        public string? MedecineTitle { get; set; }

    }
}
