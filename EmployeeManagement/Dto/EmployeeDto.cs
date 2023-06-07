using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dto
{
    public class EmployeeDto
    {
        [Key]
        public int employeeId { get; set; }

        [Required(ErrorMessage = "The product Employee Name is required.")]
        public string employeeName { get; set; }

        [Required(ErrorMessage = "The product Employee Code is required.")]
        public string employeeCode { get; set; }

        [Required(ErrorMessage = "The product Employee Salary is required.")]
        public int employeeSalary { get; set; }


        // One-to-many relationship with Employee Attendence
        public ICollection<EmployeeAttendanceDto> Attendances { get; set; }
    }
}
