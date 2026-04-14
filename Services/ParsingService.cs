using UVS_Assignment.Enums;
using UVS_Assignment.Models;
using UVS_Assignment.Services.Interfaces;

namespace UVS_Assignment.Services
{
    public class ParsingService : IParsingService
    {
        public ParsedAction Parse(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("No action provided.");
            }

            var action = args[0];

            return action switch
            {
                "get-employee" => new ParsedAction
                {
                    ActionType = ActionType.GET_EMPLOYEE,
                    GetEmployeeParsed = new GetEmployeeParsed
                    {
                        EmployeeId = GetIntValue(args, "--employeeId")
                    }
                },

                "set-employee" => new ParsedAction
                {
                    ActionType = ActionType.SET_EMPLOYEE,
                    SetEmployeeParsed = new SetEmployeeParsed
                    {
                        EmployeeId = GetIntValue(args, "--employeeId"),
                        EmployeeName = GetStringValue(args, "--employeeName"),
                        EmployeeSalary = GetIntValue(args, "--employeeSalary")
                    }
                },

                _ => throw new ArgumentException($"Unknown action: {action}")
            };
        }

        private static string GetStringValue(string[] args, string argumentName)
        {
            var index = Array.IndexOf(args, argumentName);

            if (index < 0 || index + 1 >= args.Length)
            {
                throw new ArgumentException($"Missing required argument: {argumentName}");
            }

            return args[index + 1];
        }

        private static int GetIntValue(string[] args, string argumentName)
        {
            var value = GetStringValue(args, argumentName);

            if (!int.TryParse(value, out var result))
            {
                throw new ArgumentException($"Argument {argumentName} must be a valid integer.");
            }

            return result;
        }
    }
}
