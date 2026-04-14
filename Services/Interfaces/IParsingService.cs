using UVS_Assignment.Models;

namespace UVS_Assignment.Services.Interfaces
{
    public interface IParsingService
    {
        ParsedAction Parse(string[] args);
    }
}
