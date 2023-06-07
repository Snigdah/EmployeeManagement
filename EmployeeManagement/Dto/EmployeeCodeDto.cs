using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dto
{
    public class EmployeeCodeDto
    {
        [Required(ErrorMessage = "The product Employee Code is required.")]
        public string employeeCode { get; set; } = string.Empty;
    }
}
