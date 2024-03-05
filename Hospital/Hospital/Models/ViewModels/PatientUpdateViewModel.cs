namespace Hospital.Models.ViewModels
{
    public class PatientUpdateViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
        public int? ChamberId { get; set; }

    }
}
