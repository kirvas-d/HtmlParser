using HtmlParser.HtmlLoaderService;

namespace HtmlParserTest
{
    public class HttpClientHtmlLoaderServiceTest : HtmlLoaderServiceTest
    {
        public HttpClientHtmlLoaderServiceTest() : base(new HttpClientHtmlLoaderService()) 
        {
        }
    }
}
