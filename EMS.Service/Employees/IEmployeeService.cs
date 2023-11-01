using EMS.Model.Dtos;
namespace EMS.Service.Employees
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddAsync(EmployeeDto employeeRequest);
        Task<EmployeeDto> UpdateAsync(EmployeeDto employeeRequest);
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<List<EmployeeDto>> ListAsync();
        Task<List<ShortListDto>> ShortList();

    }
}
