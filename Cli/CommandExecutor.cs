using UVS_Assignment.Cli.Enums;
using UVS_Assignment.Services;

namespace UVS_Assignment.Cli
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly ICommandParser _parsingService;
        private readonly IEmployeeService _employeeService;

        public CommandExecutor(
            ICommandParser parsingService,
            IEmployeeService employeeService)
        {
            _parsingService = parsingService;
            _employeeService = employeeService;
        }

        public async Task ExecuteAsync(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("  set-employee --employeeId <id> --employeeName <name> --employeeSalary <salary>");
                Console.WriteLine("  get-employee --employeeId <id>");
                return;
            }

            var parsedAction = _parsingService.Parse(args);

            switch (parsedAction.CommandType)
            {
                case CommandType.GET_EMPLOYEE:
                    {
                        var command = parsedAction.GetEmployeeCommand
                            ?? throw new InvalidOperationException("Get employee action was not parsed.");

                        var employee = await _employeeService.GetEmployeeAsync(command.EmployeeId);

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

                case CommandType.SET_EMPLOYEE:
                    {
                        var command = parsedAction.SetEmployeeCommand
                            ?? throw new InvalidOperationException("Set employee action was not parsed.");

                        await _employeeService.SetEmployeeAsync(
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
    }
}
