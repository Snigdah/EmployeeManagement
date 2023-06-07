using EmployeeManagement.Data;
using EmployeeManagement.Dto;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EmployeeManagement.Repository
{
    public class EmpolyeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmpolyeeRepository(DataContext context)
        {
            _context = context;
        }

        //Fetch All Employee
        public async Task<IEnumerable<Employee>> GetAllEmployees(int pageSize, int pageNumber)
        {
            try
            {
                var employee = await _context.Employees
                     .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .Include(x => x.Attendances)
                     .ToListAsync();

                return employee;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Fetch Employee By Employee ID
        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _context.Employees
                .FirstOrDefaultAsync(i => i.employeeId == id);

                return employee;
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while retrieving the employee.");
            }
        }


        // Retrieves all employees sorted by their maximum to minimum salary.
        public async Task<IEnumerable<Employee>> GetEmployeesSortedBySalary()
        {
            try
            {
                var employees = await _context.Employees
                    .OrderByDescending(e => e.employeeSalary)
                     .Include(x => x.Attendances)
                    .ToListAsync();

                return employees;
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while Employees Sorted By Salary.");
            }
        }


        //Add Employee
        public async Task AddEmployee(EmployeeDto employeeDto)
        {
            try
            {
                //Mapping Employee Model with DTO 
                var employee = new Employee
                {
                    employeeId = employeeDto.employeeId,
                    employeeName = employeeDto.employeeName,
                    employeeCode = employeeDto.employeeCode,
                    employeeSalary = employeeDto.employeeSalary,
                    Attendances = new List<EmployeeAttendance>()
                };

                //Mapping Employee Attendances
                foreach (var employeeAttendance in employeeDto.Attendances)
                {
                    var attendance = new EmployeeAttendance
                    {
                        Id = employeeAttendance.Id,
                        employeeId = employeeDto.employeeId,
                        attendanceDate = employeeAttendance.attendanceDate,
                        isPresent = employeeAttendance.isPresent,
                        isAbsent = employeeAttendance.isAbsent,
                        isOffday = employeeAttendance.isOffday,
                        Employee = null
                    };

                    employee.Attendances.Add(attendance);
                }

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while adding the Order.");
            }
        }


        // Updates the employee code of an existing employee.
        public async Task UpdateEmployeeCode(int id, EmployeeCodeDto employeeCodeDto)
        {
            try
            {
                //Fetch the employee by Employee Id
                var employee = await _context.Employees.FindAsync(id);

                if (employee != null)
                {
                    employee.employeeCode = employeeCodeDto.employeeCode;

                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while updating the employee code");
            }
        }


        // Retrieves all employees who have been absent at least one day.
        public async Task<IEnumerable<EmployeeDto>> GetAbsentEmployees()
        {
            try
            {
                // Retrieve employees who have at least one absence record
                var absentEmployees = await _context.Employees
                    .Where(e => e.Attendances.Any(a => a.isAbsent))
                    .Select(e => new EmployeeDto
                    {
                        employeeId = e.employeeId,
                        employeeName = e.employeeName,
                        employeeCode = e.employeeCode,
                        employeeSalary = e.employeeSalary
                    })
                    .ToListAsync();


                // Return the list of absent employees
                return absentEmployees;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred fetching data");
            }
        }


        // Retrieves the monthly attendance report of all employees.
        public async Task<IEnumerable<AttendanceReportDto>> GetMonthlyAttendanceReport()
        {
            try
            {
                var attendanceReport = await _context.Employees
                    .SelectMany(e => e.Attendances, (e, a) => new { Employee = e, Attendance = a })
                    .GroupBy(x => new { x.Employee.employeeName, x.Attendance.attendanceDate.Month })
                    .Select(g => new AttendanceReportDto
                    {
                         EmployeeName = g.Key.employeeName,
                         MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                         TotalPresent = g.Count(a => a.Attendance.isPresent),
                         TotalAbsent = g.Count(a => a.Attendance.isAbsent),
                         TotalOffDay = g.Count(a => a.Attendance.isOffday)
                    })
                    .ToListAsync();

                return attendanceReport;

            }
            catch (Exception)
            {

                throw new Exception("An error occurred fetching data");
            }
        }
    }
}
