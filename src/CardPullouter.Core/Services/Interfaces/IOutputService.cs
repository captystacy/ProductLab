namespace CardPullouter.Core.Services.Interfaces
{
    public interface IOutputService
    {
        Task WriteAsync(string text);
    }
}
