using EMS.Model.Dtos;
using AutoMapper;
using EMS.Model.DbModels.Employees;
using System.Linq.Expressions;
using EMS.Data.Repositories.UnitOfWork;
using EMS.Service.Users;
using EMS.Model.IdentityModel;

namespace EMS.Service.Employees
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly UserIdentity _loggedInUser;

		public EmployeeService(IUnitOfWork uow, IMapper mapper, UserSessionService userSessionService)
		{
			_uow = uow;
			_mapper = mapper;
			_loggedInUser = userSessionService.GetLoginUserInfo();
		}

		/// <summary>
		/// Adds a new employee.
		/// </summary>
		/// <param name="employeeRequest">The EmployeeDto containing employee information.</param>
		/// <returns>The newly added EmployeeDto.</returns>
		public async Task<EmployeeDto> AddAsync(EmployeeDto employeeRequest)
		{
			try
			{
				if (employeeRequest != null)
				{
					Employee employee = _mapper.Map<Employee>(employeeRequest);
					employee.IsDeleted = false;
					employee.CreatedBy = _loggedInUser.Id;
					employee.CreatedOn = DateTime.Now;
					Employee result = await _uow.EmployeeRepository.AddAsync(employee);
					EmployeeDto employeeResult = _mapper.Map<EmployeeDto>(result);
					return employeeResult;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return null;
		}

		/// <summary>
		/// Updates an existing employee.
		/// </summary>
		/// <param name="employeeRequest">The EmployeeDto containing updated employee information.</param>
		/// <returns>The updated EmployeeDto.</returns>
		public async Task<EmployeeDto> UpdateAsync(EmployeeDto employeeRequest)
		{
			bool isUpdated = false;
			try
			{
				if (employeeRequest != null && employeeRequest.Id > 0)
				{
					Employee employeeFromDb = await _uow.EmployeeRepository.GetByIdAsync(employeeRequest.Id);

					if (employeeFromDb != null && employeeFromDb.Id > 0)
					{
						employeeFromDb.FirstName = employeeRequest.FirstName;
						employeeFromDb.LastName = employeeRequest.LastName;
						employeeFromDb.MiddleName = employeeRequest.MiddleName;
						employeeFromDb.UpdatedBy = _loggedInUser.Id;
						employeeFromDb.UpdatedOn = DateTime.Now;
						Employee result = await _uow.EmployeeRepository.UpdateAsync(employeeFromDb);
						EmployeeDto employeeResult = _mapper.Map<EmployeeDto>(result);
						return employeeResult;
					}
				}
				else
					isUpdated = false;
			}
			catch (Exception ex)
			{
				throw;
			}
			return null;
		}

		/// <summary>
		/// Deletes an employee by ID.
		/// </summary>
		/// <param name="id">The ID of the employee to delete.</param>
		/// <returns>True if the employee was successfully deleted; otherwise, false.</returns>
		public async Task<bool> DeleteAsync(int id)
		{
			bool isDeleted = false;
			try
			{
				Employee employeeFromDb = await _uow.EmployeeRepository.GetByIdAsync(id);
				if (employeeFromDb != null)
				{
					await _uow.EmployeeRepository.DeleteAsync(id, _loggedInUser.Id);
					isDeleted = true;
				}
				else
					isDeleted = false;
			}
			catch (Exception ex)
			{
				throw;
			}
			return isDeleted;
		}

		/// <summary>
		/// Gets an employee by ID.
		/// </summary>
		/// <param name="id">The ID of the employee to retrieve.</param>
		/// <returns>The EmployeeDto with the specified ID.</returns>
		public async Task<EmployeeDto> GetByIdAsync(int id)
		{
			Employee employeeFromDb = await _uow.EmployeeRepository.GetByIdAsync(id);
			EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);
			return employeeDto;
		}

		/// <summary>
		/// Gets a list of all employees.
		/// </summary>
		/// <returns>A list of EmployeeDto objects.</returns>
		public async Task<List<EmployeeDto>> ListAsync()
		{
			Expression<Func<Employee, bool>> deletedFilter = entity => entity.IsDeleted == null || (Boolean)!entity.IsDeleted;
			var result = await _uow.EmployeeRepository.GetAllAsync(deletedFilter);
			List<EmployeeDto> employees = _mapper.Map<List<EmployeeDto>>(result);
			return employees;
		}

		/// <summary>
		/// Gets a list of ShortListDto objects representing employees.
		/// </summary>
		/// <returns>A list of ShortListDto objects.</returns>
		public async Task<List<ShortListDto>> ShortList()
		{
			List<ShortListDto> shortList = new();
			Expression<Func<Employee, bool>> deletedFilter = entity => entity.IsDeleted == null || (Boolean)!entity.IsDeleted;
			var result = await _uow.EmployeeRepository.GetAllAsync(deletedFilter);
			if (result != null && result.Any())
			{
				shortList = _mapper.Map<List<ShortListDto>>(result);
			}

			return shortList;


		}
	}
}
