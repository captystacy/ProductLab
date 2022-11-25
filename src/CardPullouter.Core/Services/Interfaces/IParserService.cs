using Calabonga.OperationResults;
using CardPullouter.Core.Models;

namespace CardPullouter.Core.Services.Interfaces;

public interface IParserService
{
    Task<OperationResult<IEnumerable<string>>> GetHrefsAsync(string html, HtmlElement typicalParentElement, HtmlElement targetChildElement);
    Task<OperationResult<string>> GetInnerTextAsync(string html, HtmlElement parentElement, HtmlElement targetChildElement);
}