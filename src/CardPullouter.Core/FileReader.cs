namespace CardPullouter.Core
{
    public class FileReader : IFileReader
    {
        public async Task<IEnumerable<string>> GetKeys(string path)
        {
            var fileText = await File.ReadAllTextAsync(path);

            return fileText.Split(Environment.NewLine);
        }
    }
}
