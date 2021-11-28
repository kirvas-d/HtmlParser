using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HtmlParser.HtmlLoaderService
{
    public class HttpClientHtmlLoaderService : IHtmlLoaderServiceAsync
    {
        public string GetHtmlBody(string uri)
        {
            using (var client = new HttpClient())
            {
                var response = client.Send(new HttpRequestMessage(new HttpMethod("GET"), uri));

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader streamReader = new StreamReader(response.Content.ReadAsStream())) 
                    {
                        return streamReader.ReadToEnd();
                    }
                }
                else
                {
                    throw new Exception($"Страница {uri} не бала получена. Код ошибки: {response.StatusCode}");
                }
            }
        }

        public async Task<string> GetHtmlBodyAsync(string uri)
        {
            string source = null;
           
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception($"Страница {uri} не бала получена. Код ошибки: {response.StatusCode}");
                }
            }

            return source;
        }
    }
}
