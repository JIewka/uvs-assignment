using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UVS_Assignment.Enums;
using UVS_Assignment.Infrastructure;
using UVS_Assignment.Infrastructure.Interfaces;
using UVS_Assignment.Repositories;
using UVS_Assignment.Services;
using UVS_Assignment.Services.Interfaces;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var services = new ServiceCollection();

// DbContext
services.AddDbContext<EmployeeDbContext>(options =>
    options.UseNpgsql(connectionString));

// DbContext abstraction
services.AddScoped<IEmployeeDbContext>(provider =>
    provider.GetRequiredService<EmployeeDbContext>());

// Layers
services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<IEmployeeService, EmployeeService>();

// Parsing
services.AddScoped<IParsingService, ParsingService>();

await using var serviceProvider = services.BuildServiceProvider();
await using var scope = serviceProvider.CreateAsyncScope();

try
{
    var parsingService = scope.ServiceProvider.GetRequiredService<IParsingService>();
    var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();

    var parsedAction = parsingService.Parse(args);

    switch (parsedAction.ActionType)
    {
        case ActionType.GET_EMPLOYEE:
            {
                var command = parsedAction.GetEmployeeParsed!;

                var employee = await employeeService.GetEmployeeAsync(command.EmployeeId);

                if (employee is null)
                {
                    Console.WriteLine($"Employee with ID {command.EmployeeId} was not found.");
                    return;
                }

                Console.WriteLine($"EmployeeId: {employee.Id}");
                Console.WriteLine($"EmployeeName: {employee.Name}");
                Console.WriteLine($"EmployeeSalary: {employee.Salary}");
                break;
            }

        case ActionType.SET_EMPLOYEE:
            {
                var command = parsedAction.SetEmployeeParsed!;

                await employeeService.SetEmployeeAsync(
                    command.EmployeeId,
                    command.EmployeeName,
                    command.EmployeeSalary);

                Console.WriteLine("Employee saved successfully.");
                break;
            }

        default:
            Console.WriteLine("Unknown action.");
            break;
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error occurred:");
    Console.WriteLine(ex.Message);
}