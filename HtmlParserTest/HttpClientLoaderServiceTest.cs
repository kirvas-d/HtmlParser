using HtmlParser.HtmlLoaderService;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace HtmlParserTest
{
    public class HttpClientLoaderServiceTest: IDisposable
    {
        private readonly WireMockServer server;
        private string baseUrl;
        private IWebDriver webDriver;

        public HttpClientLoaderServiceTest() 
        {
            int port = new Random().Next(5000, 6000);
            baseUrl = $"http://localhost:{port}";
            server = WireMockServer.Start( new WireMockServerSettings 
            {
                Urls = new string[] { baseUrl }
            });

            string driverPath =  Environment.GetEnvironmentVariable("SELENIUM_WEB_DRIVER_PATH", EnvironmentVariableTarget.Machine);
            if (string.IsNullOrWhiteSpace(driverPath)) 
            {
                throw new Exception("Путь к драйверу не установлен");
            }

            webDriver = new ChromeDriver(driverPath);
        }

        public void Dispose()
        {
            webDriver.Quit();
            server.Stop();
        }

        [Fact]
        public void TestHTTPClient()
        {
            string responseBody = "<html><body><h1>Hello</h1></body></html>";
            InitMockServerHtmlResponse(server, responseBody);

            HttpClientLoaderService httpClient = new HttpClientLoaderService();
            string actualResponse = httpClient.GetHtmlBody(baseUrl);

            Assert.Equal(responseBody, actualResponse);
        }

        [Fact]
        public async void TestHTTPClientAsync() 
        {
            string responseBody = "<html><body><h1>Hello</h1></body></html>";
            InitMockServerHtmlResponse(server, responseBody);

            HttpClientLoaderService httpClient = new HttpClientLoaderService();
            string actualResponse = await httpClient.GetHtmlBodyAsync(baseUrl);

            Assert.Equal(responseBody, actualResponse);
        }

        [Fact]
        public void TestLocalSeleniumLoaderService() 
        {
            string responseBody = "<html><head></head><body><h1>Hello</h1></body></html>";
            InitMockServerHtmlResponse(server, responseBody);

            using (SeleniumHtmlLoaderService seleniumHtmlLoaderService = new SeleniumHtmlLoaderService(webDriver))
            {
                string actualResponse = seleniumHtmlLoaderService.GetHtmlBody(baseUrl);
                Assert.Equal(responseBody, actualResponse);
            }
        }

        [Fact]
        public async void TestLocalSeleniumLoaderServiceAsync() 
        {
            string responseBody = "<html><head></head><body><h1>Hello</h1></body></html>";
            InitMockServerHtmlResponse(server, responseBody);

            using (SeleniumHtmlLoaderService seleniumHtmlLoaderService = new SeleniumHtmlLoaderService(webDriver))
            {
                string actualResponse = await seleniumHtmlLoaderService.GetHtmlBodyAsync(baseUrl);
                Assert.Equal(responseBody, actualResponse);
            }
        }

        private void InitMockServerHtmlResponse(IWireMockServer wireMockServer, string responseBody) 
        {
            server
                .Given(Request
                    .Create()
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "text/html; charset=UTF-8")
                    .WithBody(responseBody));
        }
    }
}
