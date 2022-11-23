using Calabonga.OperationResults;

namespace CardPullouter.Core;

public interface IFileReader
{
    Task<OperationResult<IEnumerable<string>>> GetKeys(string path);
}