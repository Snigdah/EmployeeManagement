using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // API01#
        // Upadete employee's Employee Code By Employee Id
        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateEmployeeCode(int id, [FromBody] EmployeeCodeDto employeeCodeDto)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);

                // If no employee is found with the provided id, return a 404 Not Found status with a message
                if (employee == null)
                {
                    return NotFound("This employee Id not exists");
                }

                await _employeeRepository.UpdateEmployeeCode(id, employeeCodeDto);
                return Ok(employee.employeeName + " Updated Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the employee Code.");
            }
        }


        // API02#
        // Retrieves all employees sorted by their maximum to minimum salary.
        //GET: api/Employee/sorted-by-salary
        [HttpGet("sorted-by-salary")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public async Task<IActionResult> GetEmployeesSortedBySalary()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesSortedBySalary();
                return Ok(employees);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the employees.");
            }
        }


        // API03#
        // Retrieves all employees who have been absent at least one day.
        //GET: api/Employee/absent-employees
        [HttpGet("absent-employees")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public async Task<IActionResult> GetAbsentEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAbsentEmployees();

                // If no employees are found, return a 404 Not Found status with a message
                if (employees == null)
                {
                    return NotFound("No Employee Found");
                }

                return Ok(employees);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving absent employees.");
            }
        }


        // API04#
        // Retrieves the monthly attendance report of all employees.
        //GET: api/Employee/monthlyattendance
        [HttpGet("monthlyattendance")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public async Task<IEnumerable<AttendanceReportDto>> GetMonthlyAttendanceReport()
        {
            try
            {
                var attendanceReport = await _employeeRepository.GetMonthlyAttendanceReport();

                return attendanceReport;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching the monthly attendance report.");
            }
        }

        //GET: api/AllEmployee
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public async Task<IActionResult> GetAllEmployees(int pageSize, int pageNumber)
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees(pageSize, pageNumber);

                // If no employees are found, return a 404 Not Found status with a message
                if (employees == null)
                {
                    return NotFound("No Employee Found");
                }

                // If employees are found, return a 200 OK status with the collection of employees
                return Ok(employees);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the employees.");
            }
        }


        //POST: api/AddEmployee
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                await _employeeRepository.AddEmployee(employeeDto);
                return Ok("Employee Added Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the Employee.");
            }
        }
    }
}
