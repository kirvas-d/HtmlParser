using HtmlParser.HtmlLoaderService;
using System.Collections.Generic;
using System.Linq;

namespace HtmlParser
{
    public abstract class AbstractSequentialProcessPageHtmlParser<TEntity> : AbstractHtmlParser<TEntity>
    {
        private PageHtmlParserConfiguration _configuration;

        public AbstractSequentialProcessPageHtmlParser(IHtmlLoaderService htmlLoaderService, PageHtmlParserConfiguration configuration) : base(htmlLoaderService) 
        {
            _configuration = configuration;
        }

        protected abstract IEnumerable<TEntity> GetEntityFromPage(string htmlBody);

        protected virtual string GetPageUri(int pageNumber) 
        {
            return _configuration.Uri.Replace("{page}", pageNumber.ToString());
        }

        public override IEnumerable<TEntity> GetEntitys()
        {
            int currentPageNumber = _configuration.StartPageNumber == null ? 0 : _configuration.StartPageNumber.Value;

            while (true) 
            {
                string htmlBody = _htmlLoaderService.GetHtmlBody(GetPageUri(currentPageNumber));
                List<TEntity> entities = (List<TEntity>)GetEntityFromPage(htmlBody);

                foreach (TEntity entity in entities) 
                {
                    yield return entity;
                }

                currentPageNumber++;
                if (_configuration.FinishPageNumber != null)
                {                  
                    if (currentPageNumber > _configuration.FinishPageNumber.Value)
                    {
                        break;
                    }
                }
                else if(entities.Count == 0)
                {
                    break;
                }
            }
        }
    }
}