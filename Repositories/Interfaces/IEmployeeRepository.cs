using UVS_Assignment.Models;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<bool> CheckIfExistsAsync(int employeeId, CancellationToken cancellationToken = default);
    Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    void UpdateEmployee(Employee employee);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}