﻿using HtmlParser.HtmlLoaderService;
using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace HtmlParserTest
{
    public abstract class HtmlLoaderServiceTest
    {
        protected readonly WireMockServer _wireMockServer;
        protected readonly IHtmlLoaderServiceAsync _htmlLoaderService;
        protected readonly string _baseUrl;

        public HtmlLoaderServiceTest(IHtmlLoaderServiceAsync htmlLoaderService)
        {
            _htmlLoaderService = htmlLoaderService;
            var port = new Random().Next(5000, 6000);
            _baseUrl = $"http://localhost:{port}";
            _wireMockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new string[] { _baseUrl }
            });
        }

        [Fact]
        public void TestGetHtmlBodyMethod()
        {
            string responseBody = "<html><head></head><body><h1>Hello</h1></body></html>";
            InitMockServerHtmlResponse(_wireMockServer, responseBody);

            string actualResponse = _htmlLoaderService.GetHtmlBody(_baseUrl);

            Assert.Equal(responseBody, actualResponse);
        }

        [Fact]
        public async void TestGetHtmlBodyAsyncMethod()
        {
            string responseBody = "<html><head></head><body><h1>Hello</h1></body></html>";
            InitMockServerHtmlResponse(_wireMockServer, responseBody);

            string actualResponse = await _htmlLoaderService.GetHtmlBodyAsync(_baseUrl);

            Assert.Equal(responseBody, actualResponse);

        }

        private void InitMockServerHtmlResponse(IWireMockServer wireMockServer, string responseBody)
        {
            _wireMockServer
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
