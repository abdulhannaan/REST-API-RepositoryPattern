using System.ComponentModel.DataAnnotations;

namespace EMS.Model.Dtos;

public class EmployeeDto : BaseDtoModel
{
    [Required(ErrorMessage = "FirstName is required.")]
    [StringLength(100, ErrorMessage = "FirstName cannot exceed 20 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(200, ErrorMessage = "LastName cannot exceed 20 characters.")]
    public string LastName { get; set; }

    public string? MiddleName { get; set; }
}
