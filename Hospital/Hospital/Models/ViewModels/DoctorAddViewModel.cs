namespace Hospital.Models.ViewModels
{
    public class DoctorAddViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<string> Departments { get; set; }
        public List<string> DepartmentsTitles { get; set; }
        public List<string> Positions { get; set; }
        public List<string> PositionsTitles { get; set; }

    }
}
