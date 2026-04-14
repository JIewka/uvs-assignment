using UVS_Assignment.Models;
using UVS_Assignment.Services.Interfaces;

namespace UVS_Assignment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee?> GetEmployeeAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero.", nameof(employeeId));
            }

            return await _employeeRepository.GetEmployeeByIdAsync(employeeId, cancellationToken);
        }

        public async Task SetEmployeeAsync(
            int employeeId,
            string employeeName,
            int employeeSalary,
            CancellationToken cancellationToken = default)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero.", nameof(employeeId));
            }

            if (string.IsNullOrWhiteSpace(employeeName))
            {
                throw new ArgumentException("Employee name is required.", nameof(employeeName));
            }

            if (employeeName.Length > 128)
            {
                throw new ArgumentException("Employee name must not exceed 128 characters.", nameof(employeeName));
            }

            if (employeeSalary < 0)
            {
                throw new ArgumentException("Employee salary must not be negative.", nameof(employeeSalary));
            }

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeId, cancellationToken);

            if (existingEmployee is null)
            {
                var newEmployee = new Employee
                {
                    Id = employeeId,
                    Name = employeeName,
                    Salary = employeeSalary
                };

                await _employeeRepository.AddEmployeeAsync(newEmployee, cancellationToken);
            }
            else
            {
                existingEmployee.Name = employeeName;
                existingEmployee.Salary = employeeSalary;

                _employeeRepository.UpdateEmployee(existingEmployee);
            }

            await _employeeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
