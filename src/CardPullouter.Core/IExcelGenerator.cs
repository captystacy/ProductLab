namespace CardPullouter.Core;

public interface IExcelGenerator
{
    Task CreateExcel();
    Task AddWorksheet<T>(string sheetName, IList<T> objects);
    Task SaveExcel(string filePath);
}