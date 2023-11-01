using EMS.Model.DbModels;

namespace EMS.Model.DbModels.Employees
{
    public class Employee:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
    }
}
