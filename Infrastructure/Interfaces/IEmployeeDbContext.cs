using Microsoft.EntityFrameworkCore;
using UVS_Assignment.Models;

namespace UVS_Assignment.Infrastructure.Interfaces
{
    public interface IEmployeeDbContext
    {
        DbSet<Employee> Employees { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
