using System.Threading.Tasks;

namespace HtmlParser.HtmlLoaderService
{
    public interface IHtmlLoaderService
    {
        string GetHtmlBody(string uri);
    }

    public interface IHtmlLoaderServiceAsync: IHtmlLoaderService  
    {
        Task<string> GetHtmlBodyAsync(string uri);
    }
}
