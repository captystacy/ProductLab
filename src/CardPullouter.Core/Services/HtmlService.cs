using Calabonga.OperationResults;
using CardPullouter.Core.Services.Interfaces;
using PuppeteerSharp;

namespace CardPullouter.Core.Services
{
    public class HtmlService : IHtmlService
    {
        private IBrowser? _browser;

        public async Task<OperationResult<Empty>> LoadBrowserAsync()
        {
            var operation = OperationResult.CreateResult<Empty>();

            try
            {
                _browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = @"c:\Program Files\Google\Chrome\Application\chrome.exe"
                });
            }
            catch (Exception exception)
            {
                operation.AddError("Something went wrong when launching headless browser", exception);
                return operation;
            }

            return operation;
        }

        public async Task<OperationResult<string>> LoadAndWaitForSelectorAsync(string uri, string selector)
        {
            var operation = OperationResult.CreateResult<string>();

            if (_browser is null || _browser.IsClosed)
            {
                operation.AddError("Browser was not initialized");
                return operation;
            }

            await using var page = await _browser.NewPageAsync();
            await page.GoToAsync(uri);

            try
            {
                await page.WaitForSelectorAsync(selector);
            }
            catch (Exception exception)
            {
                operation.AddError("Something went wrong when waiting for selector", exception);
                return operation;
            }

            operation.Result = await page.GetContentAsync();

            return operation;
        }

        public async Task CloseBrowserAsync()
        {
            if (_browser != null) await _browser.CloseAsync();
        }
    }
}
