using OfficeOpenXml;

namespace CardPullouter.Core
{
    public class ExcelGenerator : IExcelGenerator
    {
        private ExcelPackage _excel;

        public ExcelGenerator()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public Task CreateExcel()
        {
            _excel = new ExcelPackage();

            return Task.CompletedTask;
        }

        public Task AddWorksheet<T>(string sheetName, IList<T> objects)
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

        public Task SaveExcel(string filePath)
        {
            _excel.SaveAs(filePath);

            _excel.Dispose();

            return Task.CompletedTask;
        }
    }
}
