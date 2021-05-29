using HtmlParser.HtmlLoaderService;
using System;
using System.Collections.Generic;

namespace HtmlParser
{
    public abstract class AbstractHtmlParser<TEntity>
    {
        protected IHtmlLoaderService _htmlLoaderService;

        public AbstractHtmlParser(IHtmlLoaderService htmlloaderService) 
        {
            if (_htmlLoaderService == null)
            {
                throw new Exception("HtmlParserService равен null");
            }

            _htmlLoaderService = htmlloaderService;
        }

        public abstract IEnumerable<TEntity> GetEntitys();
    }
}
