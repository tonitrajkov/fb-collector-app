using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FbCollector.Domain;
using FbCollector.Domain.Mapper;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernateCfg;

namespace FbCollector.Services
{
    public class PageFeedService : IPageFeedService
    {
        private readonly IRepository<PageFeed> _pageFeedRepository;

        public PageFeedService()
        {
            _pageFeedRepository = ServiceLocator.Current.GetInstance<IRepository<PageFeed>>();
        }

        public void CreatePageFeed(List<PageFeedModel> feeds)
        {
            foreach (var item in feeds)
            {
                var feed = new PageFeed(item.PostId, item.Link, item.Type, item.PageId)
                {
                    FbCreatedTime = item.FbCreatedTime,
                    FbUpdatedTime = item.FbUpdatedTime,
                    Type = item.Type,
                    PostName = item.PostName,
                    PostPicture = item.PostPicture,
                    Shares = item.Shares,
                    TimeCreaded = item.TimeCreaded,
                    TimeUpdated = item.TimeUpdated
                };

                if (!string.IsNullOrEmpty(item.Message))
                    feed.Message = item.Message;

                _pageFeedRepository.Save(feed);
            }
        }

        public void DeletePageFeed(int feedId)
        {
            var feed = _pageFeedRepository.Get(feedId);
            if (feed == null)
                throw new FbException("FEED_DOESNT_EXISTS");

            _pageFeedRepository.Delete(feed);
        }

        public SearchResult<PageFeedModel> GetPageFeedsFiltered(PageFeedSearchModel model)
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

            var query = _pageFeedRepository.Query();

            Expression<Func<PageFeed, bool>> filter = PredicateBuilder.True<PageFeed>();

            if(string.IsNullOrEmpty(model.PageUrlId))
                throw new FbException("PAGE_URL_IS_EMPTY");

            filter = filter.And(x => x.PageId.ToLower() == model.PageUrlId.ToLower());

            if (!string.IsNullOrEmpty(model.SearchText))
                filter = filter.And(x => x.Message.ToLower().Contains(model.SearchText.Trim().ToLower()));

            if (model.IsUsed.HasValue)
                filter = filter.And(x => x.IsUsed == model.IsUsed.Value);

            if (!string.IsNullOrEmpty(model.Type))
                filter = filter.And(x => x.Type.ToLower() == model.Type.ToLower());

            query = query.Where(filter);

            var totalItems = query.Count();

            var start = (model.CurrentPage - 1) * model.ItemsPerPage;
            var finalQuery = query.OrderBy(x => x.DateImported).Skip(start).Take(model.ItemsPerPage);

            var result = new SearchResult<PageFeedModel>
            {
                TotalItems = totalItems,
                Items = new List<PageFeedModel>()
            };

            foreach (var item in finalQuery)
            {
                result.Items.Add(item.ToModel());
            }

            return result;
        }

        public void SetFeedAsUsed(int feedId)
        {
            var feed = _pageFeedRepository.Get(feedId);
            if (feed == null)
                throw new FbException("FEED_DOESNT_EXISTS");

            feed.IsUsed = true;
            feed.DateUsed = DateTime.Now;
            _pageFeedRepository.Update(feed);
        }
    }
}
