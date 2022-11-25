namespace CardPullouter.Core.Services.Interfaces;

public interface IExcelService : IAsyncDisposable
{
    Task CreateExcelAsync();
    Task AddWorksheetAsync<T>(string sheetName, IList<T> objects);
    Task SaveExcelAsync(string filePath);
}