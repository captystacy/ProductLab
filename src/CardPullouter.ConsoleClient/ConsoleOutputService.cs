using CardPullouter.Core.Services.Interfaces;

namespace CardPullouter.ConsoleClient
{
    public class ConsoleOutputService : IOutputService
    {
        public Task WriteAsync(string text)
        {
            Console.WriteLine(text);

            return Task.CompletedTask;
        }
    }
}
