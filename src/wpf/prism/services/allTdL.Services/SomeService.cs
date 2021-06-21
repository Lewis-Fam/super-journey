using allTdL.Services.Interfaces;

namespace allTdL.Services
{

    public class SomeService : ISomeService
    {
        public string GetMessage()
        {
            return "This is some dynamic message";
        }
    }
}