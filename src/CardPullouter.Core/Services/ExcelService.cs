using CardPullouter.Core.Services.Interfaces;
using OfficeOpenXml;

namespace CardPullouter.Core.Services
{
    public class ExcelService : IExcelService
    {
        private ExcelPackage _excel = null!;

        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public Task CreateExcelAsync()
        {
            _excel = new ExcelPackage();

            return Task.CompletedTask;
        }

        public Task AddWorksheetAsync<T>(string sheetName, IList<T> objects)
        {
            var workSheet = _excel.Workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var propertyName = properties[i].Name;

                workSheet.Cells[1, i + 1].Value = propertyName;

                for (int j = 0; j < objects.Count; j++)
                {
                    workSheet.Cells[j + 2, i + 1].Value = typeof(T).GetProperty(propertyName)?.GetValue(objects[j]);
                }
            }

            return Task.CompletedTask;
        }

        public Task SaveExcelAsync(string filePath)
        {
            _excel.SaveAs(filePath);

            return Task.CompletedTask;
        }

        public ValueTask DisposeAsync()
        {
            _excel.Dispose();

            return ValueTask.CompletedTask;
        }
    }
}
