namespace CardPullouter.Core;

public interface IFileReader
{
    Task<IEnumerable<string>> GetKeys(string path);
}