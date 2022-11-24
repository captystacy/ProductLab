using Calabonga.OperationResults;

namespace CardPullouter.Core.Interfaces;

public interface IFileReader
{
    Task<OperationResult<IEnumerable<string>>> GetKeys(string path);
}