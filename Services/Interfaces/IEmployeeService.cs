using UVS_Assignment.Models;

namespace UVS_Assignment.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee?> GetEmployeeAsync(int employeeId, CancellationToken cancellationToken = default);
        Task SetEmployeeAsync(int employeeId, string employeeName, int employeeSalary, CancellationToken cancellationToken = default);
    }
}
