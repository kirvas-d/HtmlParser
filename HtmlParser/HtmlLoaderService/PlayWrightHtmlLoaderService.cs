using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser.HtmlLoaderService
{
    public class PlayWrightHtmlLoaderService : IHtmlLoaderServiceAsync, IDisposable
    {
        private readonly IPlaywright _playwright;
        private readonly IBrowser _browser;

        public PlayWrightHtmlLoaderService() 
        {
            _playwright = Playwright.CreateAsync().GetAwaiter().GetResult();
            _browser = _playwright.Chromium.LaunchAsync().GetAwaiter().GetResult();
            
        }

        public void Dispose()
        {
            _browser.DisposeAsync().GetAwaiter().GetResult();
            _playwright.Dispose();
        }

        public string GetHtmlBody(string uri)
        {
            return GetHtmlBodyAsync(uri).GetAwaiter().GetResult();
        }

        public async Task<string> GetHtmlBodyAsync(string uri)
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(uri, new PageGotoOptions() { Timeout = 60000 });
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions() { Timeout = 60000 });
            var htmlContent = await page.ContentAsync();
            await page.CloseAsync();

            return htmlContent;
        }
    }
}
