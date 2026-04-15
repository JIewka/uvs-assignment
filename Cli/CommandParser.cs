using UVS_Assignment.Cli.Enums;
using UVS_Assignment.Cli.Models;

namespace UVS_Assignment.Cli
{
    public class CommandParser : ICommandParser
    {
        public ParsedCommand Parse(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("No command provided.");
            }

            var command = args[0];

            return command switch
            {
                "get-employee" => new ParsedCommand
                {
                    CommandType = CommandType.GET_EMPLOYEE,
                    GetEmployeeCommand = new GetEmployeeCommand
                    {
                        EmployeeId = GetIntValue(args, "--employeeId")
                    }
                },

                "set-employee" => new ParsedCommand
                {
                    CommandType = CommandType.SET_EMPLOYEE,
                    SetEmployeeCommand = new SetEmployeeCommand
                    {
                        EmployeeId = GetIntValue(args, "--employeeId"),
                        EmployeeName = GetStringValue(args, "--employeeName"),
                        EmployeeSalary = GetIntValue(args, "--employeeSalary")
                    }
                },

                _ => throw new ArgumentException($"Unknown command: {command}")
            };
        }

        private static string GetStringValue(string[] args, string argumentName)
        {
            var index = Array.IndexOf(args, argumentName);

            if (index < 0 || index + 1 >= args.Length)
            {
                throw new ArgumentException($"Missing argument: {argumentName}");
            }

            return args[index + 1];
        }

        private static int GetIntValue(string[] args, string argumentName)
        {
            var value = GetStringValue(args, argumentName);

            if (!int.TryParse(value, out var result))
            {
                throw new ArgumentException($"Argument {argumentName} must be an integer.");
            }

            return result;
        }
    }
}
