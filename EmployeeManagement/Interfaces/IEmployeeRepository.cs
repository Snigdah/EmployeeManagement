using EmployeeManagement.Dto;
using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        // Retrieves a collection of employees based on the provided page size and page number.
        Task<IEnumerable<Employee>> GetAllEmployees(int pageSize, int pageNumber);

        // Retrieves an employee by their unique identifier.
        Task<Employee> GetEmployeeById(int id);

        // Adds a new employee based on the provided EmployeeDto object.
        Task AddEmployee(EmployeeDto employeeDto);

        // Updates the employee code of an existing employee.
        Task UpdateEmployeeCode(int id, EmployeeCodeDto employeeCodeDto);

        // Retrieves all employees sorted by their maximum to minimum salary.
        Task<IEnumerable<Employee>> GetEmployeesSortedBySalary();

        // Retrieves all employees who have been absent at least one day.
        Task<IEnumerable<EmployeeDto>> GetAbsentEmployees();

        // Retrieves the monthly attendance report of all employees.
        Task<IEnumerable<AttendanceReportDto>> GetMonthlyAttendanceReport();

    }
}
