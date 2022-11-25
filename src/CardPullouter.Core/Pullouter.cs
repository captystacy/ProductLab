using Calabonga.OperationResults;
using CardPullouter.Core.Models;
using CardPullouter.Core.Services.Interfaces;
using System.Text.RegularExpressions;

namespace CardPullouter.Core
{
    public class Pullouter
    {
        private readonly IExcelService _excelService;
        private readonly IHtmlService _htmlService;
        private readonly IParserService _parserService;
        private readonly IOutputService _outputService;
        private readonly IFileService _fileService;

        public Pullouter(
            IExcelService excelService, 
            IFileService fileService, 
            IHtmlService htmlService,
            IParserService parserService,
            IOutputService outputService)
        {
            _excelService = excelService;
            _htmlService = htmlService;
            _parserService = parserService;
            _outputService = outputService;
            _fileService = fileService;
        }

        public async Task StartAsync()
        {
            var path = await GetRootPathAsync(Constants.KeysFileName);

            var getKeysOperation = await _fileService.GetKeysAsync(path);

            if (!getKeysOperation.Ok || getKeysOperation.Result is null)
            {
                await StopServicesAsync(getKeysOperation.GetMetadataMessages());
                return;
            }

            var keys = getKeysOperation.Result;

            await _excelService.CreateExcelAsync();
            await _htmlService.LoadBrowserAsync();

            foreach (var key in keys)
            {
                var uri = $"https://www.wildberries.ru/catalog/0/search.aspx?sort=popular&search={key}";

                var loadAndWaitForSelectorOperation = await _htmlService.LoadAndWaitForSelectorAsync(uri, Constants.CardParentElement.ToString());

                if (!loadAndWaitForSelectorOperation.Ok || loadAndWaitForSelectorOperation.Result is null)
                {
                    await StopServicesAsync(loadAndWaitForSelectorOperation.GetMetadataMessages());
                    return;
                }

                var html = loadAndWaitForSelectorOperation.Result;

                var getHrefsOperation = await _parserService.GetHrefsAsync(html, Constants.CardParentElement, Constants.LinkChildElement);

                if (!getHrefsOperation.Ok || getHrefsOperation.Result is null)
                {
                    await StopServicesAsync(getHrefsOperation.GetMetadataMessages());
                    return;
                }

                var hrefs = getHrefsOperation.Result;

                var cards = new List<Card>();
                foreach (var href in hrefs)
                {
                    var againLoadAndWaitForSelectorOperation = await _htmlService.LoadAndWaitForSelectorAsync(href, Constants.BrandChildElement.ToString());

                    if (!againLoadAndWaitForSelectorOperation.Ok || againLoadAndWaitForSelectorOperation.Result is null)
                    {
                        await StopServicesAsync(againLoadAndWaitForSelectorOperation.GetMetadataMessages());
                        return;
                    }

                    var againHtml = againLoadAndWaitForSelectorOperation.Result;

                    var getCardOperation = await GetCardAsync(againHtml);

                    if (!getCardOperation.Ok || getCardOperation.Result is null)
                    {
                        await StopServicesAsync(getCardOperation.GetMetadataMessages());
                        return;
                    }

                    var card = getCardOperation.Result;

                    cards.Add(card);

                    await _outputService.WriteAsync(card.ToString());
                }

                await _excelService.AddWorksheetAsync(key, cards);
            }

            var savePath = await GetRootPathAsync(Constants.ResultExcelFileName);
            await _excelService.SaveExcelAsync(savePath);

            await StopServicesAsync("Excel file was successfully created.");
        }

        private Task<string> GetRootPathAsync(string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDirectory, fileName);
            return Task.FromResult(path);
        }

        private async Task<OperationResult<Card>> GetCardAsync(string html)
        {
            var operation = OperationResult.CreateResult<Card>();

            var getTitleInnerTextOperation = await _parserService.GetInnerTextAsync(html, Constants.TitleParentElement, Constants.TitleChildElement);

            if (!getTitleInnerTextOperation.Ok || string.IsNullOrEmpty(getTitleInnerTextOperation.Result))
            {
                operation.AddError(getTitleInnerTextOperation.GetMetadataMessages());
                return operation;
            }
            var title = getTitleInnerTextOperation.Result;

            var getBrandInnerTextOperation = await _parserService.GetInnerTextAsync(html, Constants.BrandParentElement, Constants.BrandChildElement);

            if (!getBrandInnerTextOperation.Ok || string.IsNullOrEmpty(getBrandInnerTextOperation.Result))
            {
                operation.AddError(getBrandInnerTextOperation.GetMetadataMessages());
                return operation;
            }
            var brand = getBrandInnerTextOperation.Result;

            var getIdInnerTextOperation = await _parserService.GetInnerTextAsync(html, Constants.IdParentElement, Constants.IdChildElement);

            if (!getIdInnerTextOperation.Ok || string.IsNullOrEmpty(getIdInnerTextOperation.Result))
            {
                operation.AddError(getIdInnerTextOperation.GetMetadataMessages());
                return operation;
            }

            var idStr = getIdInnerTextOperation.Result;

            if (!long.TryParse(idStr, out var id))
            {
                operation.AddError("Cant parse card id " + idStr);
                return operation;
            }

            var getFeedbacksInnerTextOperation = await _parserService.GetInnerTextAsync(html, Constants.FeedbacksParentElement, Constants.FeedbacksChildElement);

            if (!getFeedbacksInnerTextOperation.Ok || string.IsNullOrEmpty(getFeedbacksInnerTextOperation.Result))
            {
                operation.AddError(getFeedbacksInnerTextOperation.GetMetadataMessages());
                return operation;
            }

            var feedbacksStr = getFeedbacksInnerTextOperation.Result;

            var feedbacksDigitsStr = string.Concat(Regex.Matches(feedbacksStr, @"\d+").Select(x => x.Value));

            if (!int.TryParse(feedbacksDigitsStr, out var feedbacks))
            {
                operation.AddError("Cant parse card feedbacks " + feedbacksStr);
                return operation;
            }

            var getPriceInnerTextOperation = await _parserService.GetInnerTextAsync(html, Constants.PriceParentElement, Constants.PriceChildElement);

            if (!getPriceInnerTextOperation.Ok || string.IsNullOrEmpty(getPriceInnerTextOperation.Result))
            {
                operation.AddError(getPriceInnerTextOperation.GetMetadataMessages());
                return operation;
            }

            var priceStr = getPriceInnerTextOperation.Result;

            var priceDigitsStr = string.Concat(Regex.Matches(priceStr, @"\d+").Select(x => x.Value));

            if (!decimal.TryParse(priceDigitsStr, out var price))
            {
                operation.AddError("Cant parse card price " + priceStr);
                return operation;
            }

            operation.Result = new Card(title, brand, id, feedbacks, price);

            return operation;
        }

        private async Task StopServicesAsync(string textToOutput)
        {
            await _htmlService.CloseBrowserAsync();
            await _excelService.DisposeAsync();
            await _outputService.WriteAsync(textToOutput);
        }
    }
}

