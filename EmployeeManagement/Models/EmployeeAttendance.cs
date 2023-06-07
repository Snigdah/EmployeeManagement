using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class EmployeeAttendance
    {
        [Key]
        public int Id { get; set; }

        public int employeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime attendanceDate { get; set; }
        public bool isPresent { get; set; }
        public bool isAbsent { get; set; }
        public bool isOffday { get; set; }
    }
}
