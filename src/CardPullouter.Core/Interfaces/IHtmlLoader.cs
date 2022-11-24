using Calabonga.OperationResults;

namespace CardPullouter.Core.Interfaces;

public interface IHtmlLoader
{
    Task<OperationResult<string>> LoadAndWaitForSelector(string uri, string selector);
}