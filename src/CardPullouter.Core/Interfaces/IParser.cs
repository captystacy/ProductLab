using Calabonga.OperationResults;

namespace CardPullouter.Core.Interfaces;

public interface IParser
{
    Task<OperationResult<IEnumerable<string>>> GetHrefs(string html, HtmlElement typicalParentElement, HtmlElement targetChildElement);
}