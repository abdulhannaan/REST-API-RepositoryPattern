using EMS.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using EMS.Service.Employees;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EMS.Controllers
{
	[Authorize]
	[Route("api/employees")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;

		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		/// <summary>
		/// Create a new employee.
		/// </summary>
		/// <param name="request">The EmployeeDto containing employee information.</param>
		/// <returns>The IActionResult with appropriate response.</returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] EmployeeDto request)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await _employeeService.AddAsync(request);

					return Ok(ApiResponseModel.GetResponse("Employee added successfully.", HttpStatusCode.OK, result));
				}
				else
					return BadRequest(ApiResponseModel.GetResponse("Model is Not Valid", HttpStatusCode.BadRequest, ModelState));
			}
			catch (Exception ex)
			{
				return BadRequest(ApiResponseModel.GetResponse($"Request Failed. Error: {ex.Message}", HttpStatusCode.BadRequest, false));
			}
		}

		/// <summary>
		/// Update an existing employee.
		/// </summary>
		/// <param name="id">The ID of the employee to update.</param>
		/// <param name="request">The EmployeeDto containing updated employee information.</param>
		/// <returns>The IActionResult with appropriate response.</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto request)
		{
			try
			{
				if (ModelState.IsValid)
				{
					request.Id = id;
					var result = await _employeeService.UpdateAsync(request);
					if (result != null && result.Id > 0)
						return Ok(ApiResponseModel.GetResponse("Employee updated successfully.", HttpStatusCode.OK, result));
					else
						return Ok(ApiResponseModel.GetResponse("Failed to update. Employee not found.", HttpStatusCode.NotModified, result));
				}
				else
					return BadRequest(ApiResponseModel.GetResponse("Model is Not Valid", HttpStatusCode.BadRequest, ModelState));
			}
			catch (Exception ex)
			{
				return BadRequest(ApiResponseModel.GetResponse($"Request Failed. Error: {ex.Message}", HttpStatusCode.BadRequest, false));
			}
		}

		/// <summary>
		/// Delete an employee by ID.
		/// </summary>
		/// <param name="id">The ID of the employee to delete.</param>
		/// <returns>The IActionResult with appropriate response.</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				bool isDeleted = await _employeeService.DeleteAsync(id);
				if (isDeleted)
					return Ok(ApiResponseModel.GetResponse("Employee deleted successfully.", HttpStatusCode.OK, isDeleted));
				else
					return Ok(ApiResponseModel.GetResponse("Failed to delete. Employee not found.", HttpStatusCode.NotModified, isDeleted));
			}
			catch (Exception ex)
			{
				return BadRequest(ApiResponseModel.GetResponse($"Request Failed. Error: {ex.Message}", HttpStatusCode.BadRequest, false));
			}
		}

		/// <summary>
		/// Get an employee by ID.
		/// </summary>
		/// <param name="id">The ID of the employee to retrieve.</param>
		/// <returns>The IActionResult with appropriate response.</returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				EmployeeDto employee = await _employeeService.GetByIdAsync(id);
				return Ok(ApiResponseModel.GetResponse("Employee Found.", HttpStatusCode.OK, employee));
			}
			catch (Exception ex)
			{
				return BadRequest(ApiResponseModel.GetResponse($"Request Failed. Error: {ex.Message}", HttpStatusCode.BadRequest));
			}
		}

		/// <summary>
		/// Get a list of all employees.
		/// </summary>
		/// <returns>The IActionResult with appropriate response.</returns>
		[HttpGet]
		public async Task<IActionResult> List()
		{
			try
			{
				var result = await _employeeService.ListAsync();
				return Ok(ApiResponseModel.GetResponse("Employees Found.", HttpStatusCode.OK, result));
			}
			catch (Exception ex)
			{
				return BadRequest(ApiResponseModel.GetResponse($"Request Failed. Error: {ex.Message}", HttpStatusCode.BadRequest));
			}
		}
	}
}
