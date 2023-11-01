
using EMS.Data.Repositories.Employees;

namespace EMS.Data.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
    }
}
