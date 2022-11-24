using Calabonga.OperationResults;
using CardPullouter.Core.Interfaces;

namespace CardPullouter.Core
{
    public class FileReader : IFileReader
    {
        public async Task<OperationResult<IEnumerable<string>>> GetKeys(string path)
        {
            var operation = OperationResult.CreateResult<IEnumerable<string>>();

            string fileText;
            try
            {
                fileText = await File.ReadAllTextAsync(path);
            }
            catch (Exception exception)
            {
                operation.AddError("Something went wrong when trying to read file", exception);
                return operation;
            }

            if (string.IsNullOrEmpty(fileText))
            {
                operation.AddError("File was empty");
            }

            operation.Result = fileText.Split(Environment.NewLine);

            return operation;
        }
    }
}
