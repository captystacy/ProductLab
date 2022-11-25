using Calabonga.OperationResults;
using CardPullouter.Core.Interfaces;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace CardPullouter.Core
{
    public class Parser : IParser
    {
        private const string HrefAttributeName = "href";

        public async Task<OperationResult<IEnumerable<string>>> GetHrefs(string html, HtmlElement typicalParentElement, HtmlElement targetChildElement)
        {
            var operation = OperationResult.CreateResult<IEnumerable<string>>();

            var getElementsOperation = await GetElements(html, typicalParentElement);

            if (!getElementsOperation.Ok || getElementsOperation.Result is null)
            {
                operation.AddError(getElementsOperation.GetMetadataMessages());
                return operation;
            }

            var elements = getElementsOperation.Result;

            var linkElements = elements.Select(x => x.QuerySelector(targetChildElement.ToString())).ToList();

            if (linkElements.Count == 0)
            {
                operation.AddError($"There is no {targetChildElement} in {typicalParentElement}");
                return operation;
            }

            var hrefs = linkElements.Select(x => x.Attributes[HrefAttributeName].Value).ToList();

            if (hrefs.Count == 0)
            {
                operation.AddError($"There is no hrefs in {typicalParentElement}");
                return operation;
            }

            operation.Result = hrefs;

            return operation;
        }

        private Task<OperationResult<IList<HtmlNode>>> GetElements(string html, HtmlElement typicalParentElement)
        {
            var operation = OperationResult.CreateResult<IList<HtmlNode>>();

            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var elements = htmlDocument.DocumentNode.QuerySelectorAll(typicalParentElement.ToString());

            if (elements.Count == 0)
            {
                operation.AddError($"There is no {typicalParentElement}");
                return Task.FromResult(operation);
            }

            operation.Result = elements;

            return Task.FromResult(operation);
        }

        public async Task<OperationResult<string>> GetInnerText(string html, HtmlElement parentElement, HtmlElement targetChildElement)
        {
            var operation = OperationResult.CreateResult<string>();

            var getElementsOperation = await GetElements(html, parentElement);

            if (!getElementsOperation.Ok || getElementsOperation.Result is null)
            {
                operation.AddError(getElementsOperation.GetMetadataMessages());
                return operation;
            }

            var elements = getElementsOperation.Result;

            if (elements.Count > 1)
            {
                operation.AddError($"Were found more than one parent element, please specify your parent. Parent: {parentElement}, child {targetChildElement}");
                return operation;
            }

            var parent = elements.First();

            HtmlNode target;
            try
            {
                target = parent.QuerySelector(targetChildElement.ToString());
            }
            catch (Exception exception)
            {
                operation.AddError($"Specified target child was not found. Parent: {parentElement}, child {targetChildElement}", exception);
                return operation;
            }

            if (target is null)
            {
                operation.AddError($"Specified target child was not found. Parent: {parentElement}, child {targetChildElement}");
                return operation;
            }

            if (string.IsNullOrEmpty(target.InnerText))
            {
                operation.AddError($"Inner text of {targetChildElement} was null or empty. Parent: {parentElement}, child {targetChildElement}");
                return operation;
            }

            operation.Result = target.InnerText.Trim();

            return operation;
        }
    }
}
