using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dto
{
    public class EmployeeAttendanceDto
    {
        [Key]
        public int Id { get; set; }

        public DateTime attendanceDate { get; set; }
        public bool isPresent { get; set; }
        public bool isAbsent { get; set; }
        public bool isOffday { get; set; }
    }
}
