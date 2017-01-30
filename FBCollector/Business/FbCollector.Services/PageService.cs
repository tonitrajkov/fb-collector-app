using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FbCollector.Domain;
using FbCollector.Domain.Mapper;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernateCfg;

namespace FbCollector.Services
{
    public class PageService : IPageService
    {
        private readonly IRepository<Page> _pageRepository;

        public PageService()
        {
            _pageRepository = ServiceLocator.Current.GetInstance<IRepository<Page>>();
        }

        public void CreatePage(PageModel model)
        {
            var page = _pageRepository.Query()
                .FirstOrDefault(p => p.UrlId.ToLower() == model.UrlId.ToLower());

            if(page != null)
                throw new FbException("PAGE_ALREDY_EXISTS");

            var domain = new Page(model.Title, model.Url, model.UrlId);

            if (!string.IsNullOrEmpty(domain.FbType))
                domain.FbType = model.FbType;
            if (!string.IsNullOrEmpty(domain.FbId))
                domain.FbId = model.FbId;

            _pageRepository.Save(domain);
        }

        public void UpdatePage(PageModel model)
        {
            var page = _pageRepository.Get(model.Id);
            if (page == null)
                throw new FbException("PAGE_DOESNT_EXISTS");

            page.FbType = model.FbType;
            page.FbId = model.FbId;
            page.UrlId = model.UrlId;
            page.Url = model.Url;
            page.Title = model.Title;
            
            _pageRepository.Update(page);
        }

        public void DeletePage(int pageId)
        {
            var page = _pageRepository.Get(pageId);
            if (page == null)
                throw new FbException("PAGE_DOESNT_EXISTS");

            _pageRepository.Delete(page);
        }

        public SearchResult<PageModel> GetPagesFiltered(PageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.ItemsPerPage > 50)
            {
                model.ItemsPerPage = 50;
            }

            if (model.ItemsPerPage < 0)
            {
                model.ItemsPerPage = 5;
            }

            var query = _pageRepository.Query();

            Expression<Func<Page, bool>> filter = PredicateBuilder.True<Page>();

            if (!string.IsNullOrEmpty(model.SearchText))
            {
                filter = filter.And(x => x.Title.ToLower().Contains(model.SearchText.Trim().ToLower()))
                               .Or(x => x.Url.ToLower().Contains(model.SearchText.Trim().ToLower()))
                               .Or(x => x.UrlId.ToLower().Contains(model.SearchText.Trim().ToLower()));
            }

            query = query.Where(filter);

            var totalItems = query.Count();

            var start = (model.CurrentPage - 1) * model.ItemsPerPage;
            var finalQuery = query.OrderBy(x => x.DateCreated).Skip(start).Take(model.ItemsPerPage);

            var result = new SearchResult<PageModel>
            {
                TotalItems = totalItems,
                Items = new List<PageModel>()
            };

            foreach (var item in finalQuery)
            {
                result.Items.Add(item.ToModel());
            }

            return result;
        }
    }
}
