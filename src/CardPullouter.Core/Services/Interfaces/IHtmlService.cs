using Calabonga.OperationResults;

namespace CardPullouter.Core.Services.Interfaces;

public interface IHtmlService
{
    Task<OperationResult<Empty>> LoadBrowserAsync();
    Task<OperationResult<string>> LoadAndWaitForSelectorAsync(string uri, string selector);
    Task CloseBrowserAsync();
}