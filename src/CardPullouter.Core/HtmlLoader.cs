using Calabonga.OperationResults;
using CardPullouter.Core.Interfaces;
using PuppeteerSharp;

namespace CardPullouter.Core
{
    public class HtmlLoader : IHtmlLoader
    {
        public async Task<OperationResult<string>> LoadAndWaitForSelector(string uri, string selector)
        {
            var operation = OperationResult.CreateResult<string>();

            IBrowser browser;
            try
            {
                browser = await Puppeteer.LaunchAsync(new LaunchOptions
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

            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(uri);
            await page.WaitForSelectorAsync(selector);

            operation.Result = await page.GetContentAsync();

            return operation;
        }
    }
}
