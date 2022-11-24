using Calabonga.OperationResults;

namespace CardPullouter.Core.Interfaces;

public interface IParser
{
    Task<OperationResult<IEnumerable<string>>> GetHrefs(string html, HtmlElement typicalParentElement, HtmlElement targetChildElement);
    Task<OperationResult<string>> GetInnerText(string html, HtmlElement parentElement, HtmlElement targetChildElement);
}