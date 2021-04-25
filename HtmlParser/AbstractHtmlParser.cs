using HtmlParser.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    public class AbstractHtmlParser<TEntity>
    {
        private IHtmlScraperService htmlScraperService;
        private IHtmlParserService<TEntity> htmlParserService;

        public AbstractHtmlParser(IHtmlParserService<TEntity> htmlParserService, IHtmlScraperService htmlScraperService) 
        {
            this.htmlParserService = htmlParserService;
            if (this.htmlParserService == null) 
            {
                throw new Exception("HtmlParserService равен null");
            }

            this.htmlScraperService = htmlScraperService;
            if (this.htmlScraperService == null)
            {
                throw new Exception("HtmlParserService равен null");
            }
        }

        public IEnumerable<TEntity> GetEntity() 
        {
            throw new Exception();
        }

    }
}
