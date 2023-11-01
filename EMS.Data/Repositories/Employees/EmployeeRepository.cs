using EMS.Model.DbModels;
using EMS.Model.DbModels.Employees;

namespace EMS.Data.Repositories.Employees
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly EMSContext _dbContext;

        public EmployeeRepository(EMSContext EMSContext) : base(EMSContext)
        {
            _dbContext = EMSContext;
        }
    }
}
