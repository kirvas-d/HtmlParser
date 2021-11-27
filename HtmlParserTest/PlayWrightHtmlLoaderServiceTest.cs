using HtmlParser.HtmlLoaderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
