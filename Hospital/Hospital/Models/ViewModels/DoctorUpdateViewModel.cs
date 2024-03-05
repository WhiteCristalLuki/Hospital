namespace Hospital.Models.ViewModels
{
    public class DoctorUpdateViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<string> Departments { get; set; }
        public List<string> Positions { get; set; }
    }
}
