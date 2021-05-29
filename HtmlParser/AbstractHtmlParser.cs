using HtmlParser.HtmlLoaderService;
using System;
using System.Collections.Generic;

namespace HtmlParser
{
    public abstract class AbstractHtmlParser<TEntity>
    {
        protected IHtmlLoaderService _htmlLoaderService;

        public AbstractHtmlParser(IHtmlLoaderService htmlLoaderService) 
        {
            if (htmlLoaderService == null)
            {
                throw new Exception("HtmlLoaderService равен null");
            }

            _htmlLoaderService = htmlLoaderService;
        }

        public abstract IEnumerable<TEntity> GetEntitys();
    }
}
