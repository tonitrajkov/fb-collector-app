using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FbCollector.Domain;
using FbCollector.Domain.Mapper;
using FbCollector.Infrastructure;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernateCfg;
using NHibernate;
using NHibernate.Transform;

namespace FbCollector.Services
{
    public class PageFeedService : IPageFeedService
    {
        private readonly IRepository<PageFeed> _pageFeedRepository;

        public PageFeedService()
        {
            _pageFeedRepository = ServiceLocator.Current.GetInstance<IRepository<PageFeed>>();
        }

        public void CreatePageFeed(List<FbFeedModel> feeds, string pageUrlId)
        {
            foreach (var item in feeds)
            {
                var feed = new PageFeed(item.id, item.link, item.type, pageUrlId)
                {
                    FbCreatedTime = item.created_time,
                    FbUpdatedTime = item.updated_time,
                    Type = item.type,
                    PostName = item.name,
                    PostPicture = item.full_picture,
                    Shares = (item.shares != null && item.shares.count.HasValue) ? item.shares.count.Value : 0,
                    TimeCreaded = !string.IsNullOrEmpty(item.created_time) ?
                                        DateTime.Parse(item.created_time) : (DateTime?)null,
                    TimeUpdated = !string.IsNullOrEmpty(item.updated_time) ?
                                        DateTime.Parse(item.updated_time) : (DateTime?)null,
                    Message = item.message
                };

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

            if (string.IsNullOrEmpty(model.PageUrlId))
                throw new FbException("PAGE_URL_IS_EMPTY");

            filter = filter.And(x => x.PageId.ToLower() == model.PageUrlId.ToLower());

            if (!string.IsNullOrEmpty(model.SearchText))
                filter = filter.And(x => x.Message.ToLower().Contains(model.SearchText.Trim().ToLower()));

            if (model.IsUsed.HasValue)
                filter = filter.And(x => x.IsUsed == model.IsUsed.Value);

            if (!string.IsNullOrEmpty(model.Type))
                filter = filter.And(x => x.Type.ToLower() == model.Type.ToLower());

            if (model.DateFrom.HasValue)
                filter = filter.And(x => x.TimeCreaded.Value >= model.DateFrom.Value);

            if (model.DateTo.HasValue)
                filter = filter.And(x => x.TimeCreaded.Value <= model.DateTo.Value);

            if (model.SharesNumber.HasValue)
                filter = filter.And(x => x.Shares >= model.SharesNumber.Value);

            query = query.Where(filter);

            var totalItems = query.Count();

            var start = (model.CurrentPage - 1) * model.ItemsPerPage;

            query = model.OrderDescending ?
                        query.OrderByDescending(x => x.TimeCreaded).Skip(start).Take(model.ItemsPerPage)
                            : query.OrderBy(x => x.TimeCreaded).Skip(start).Take(model.ItemsPerPage);

            var result = new SearchResult<PageFeedModel>
            {
                TotalItems = totalItems,
                Items = new List<PageFeedModel>()
            };

            foreach (var item in query)
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

        public long? GetLastPageFeedDate(string pageUrlId)
        {
            var feed = _pageFeedRepository.Query()
                        .OrderByDescending(f => f.TimeCreaded)
                        .FirstOrDefault(f => f.PageId.ToLower() == pageUrlId.ToLower());
            if (feed == null)
                return null;

            if (!feed.TimeCreaded.HasValue)
                return null;

            return Internals.GetTime(feed.TimeCreaded.Value);
        }

        public List<PageFeedChartModel> PageFeedGroupedByHourAndType(PageFeedSearchModel model)
        {
            var _nhUnitOfWork = new NhUnitOfWork();
            _nhUnitOfWork.OpenSession();

            var query = _nhUnitOfWork.Session
                .GetNamedQuery("PageFeedGroupedByHourAndType");

            if (!string.IsNullOrEmpty(model.PageUrlId))
                query.SetString("pageId", model.PageUrlId);
            else query.SetParameter("pageId", null, NHibernateUtil.String);

            if (model.DateFrom.HasValue)
                query.SetDateTime("dateFrom", model.DateFrom.Value);
            else query.SetParameter("dateFrom", null, NHibernateUtil.DateTime);

            if (model.DateTo.HasValue)
                query.SetDateTime("dateTo", model.DateTo.Value);
            else query.SetParameter("dateTo", null, NHibernateUtil.DateTime);

            if (!string.IsNullOrEmpty(model.Type))
                query.SetString("postType", model.Type);
            else query.SetParameter("postType", null, NHibernateUtil.String);

            if (model.Year.HasValue)
                query.SetInt32("year", model.Year.Value);
            else query.SetParameter("year", null, NHibernateUtil.Int32);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(PageFeedChartModel)));
            query.SetReadOnly(true);
            var result = query.List<PageFeedChartModel>().ToList();

            return result;
        }
    }
}
