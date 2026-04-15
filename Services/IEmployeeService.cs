using UVS_Assignment.Dtos;
using UVS_Assignment.Entities;

namespace UVS_Assignment.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
        Task SetEmployeeAsync(int employeeId, string employeeName, int employeeSalary, CancellationToken cancellationToken = default);
    }
}
