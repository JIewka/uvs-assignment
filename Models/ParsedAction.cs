using UVS_Assignment.Enums;

namespace UVS_Assignment.Models
{
    public class ParsedAction
    {
        public ActionType ActionType { get; set; }

        public GetEmployeeParsed? GetEmployeeParsed { get; set; }
        public SetEmployeeParsed? SetEmployeeParsed { get; set; }
    }
}
