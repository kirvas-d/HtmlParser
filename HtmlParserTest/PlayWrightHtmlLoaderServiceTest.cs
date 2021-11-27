using HtmlParser.HtmlLoaderService;
using System;

namespace HtmlParserTest
{
    public class PlayWrightHtmlLoaderServiceTest : HtmlLoaderServiceTest, IDisposable
    {
        public PlayWrightHtmlLoaderServiceTest() : base(new PlayWrightHtmlLoaderService())
        {
        }

        public void Dispose()
        {
            ((PlayWrightHtmlLoaderService)_htmlLoaderService).Dispose();
        }
    }
}
