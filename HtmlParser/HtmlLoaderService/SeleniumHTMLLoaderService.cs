using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlParser.HtmlLoaderService
{
    public class SeleniumHtmlLoaderService : IHtmlLoaderServiceAsync, IDisposable
    {
        private readonly IWebDriver _driver;
        private bool _isDispose;

        public SeleniumHtmlLoaderService(IWebDriver driver) 
        {
            this._driver = driver;
            _isDispose = false;
        }

        public string GetHtmlBody(string uri)
        {
            _driver.Navigate().GoToUrl(uri);
            Thread.Sleep(100 * new Random().Next(50, 100));
            WaitForPageLoad();

            return _driver.PageSource;
        }

        public async Task<string> GetHtmlBodyAsync(string uri)
        {
            string resultHtml = null;
            await Task.Run(() =>
            {
                resultHtml = GetHtmlBody(uri);
            });

            return resultHtml;
        }

        private void WaitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void Dispose()
        {
            if (!_isDispose) 
            {
                _driver.Quit();
                _isDispose = true;
            }
        }
    }
}
