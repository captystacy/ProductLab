using Calabonga.OperationResults;

namespace CardPullouter.Core.Services.Interfaces;

public interface IFileService
{
    Task<OperationResult<IEnumerable<string>>> GetKeysAsync(string path);
}