using System.Linq;
using FbCollector.Models;

namespace FbCollector.Domain.Mapper
{
    public static class MapExtensions
    {
        public static UserModel ToModel(this User user)
        {
            var model = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                Active = user.Active,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Telephone = user.Telephone,
                ProfilePicture = user.ProfilePicture,
                Roles = (user.Roles != null && user.Roles.Count > 0) ? user.Roles.Select(r => r.ToModel()).ToList() : null
            };

            return model;
        }

        public static LoggedInUserInfo ToLoggedInUserModel(this User user)
        {
            var model = new LoggedInUserInfo
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                Active = user.Active,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Telephone = user.Telephone,
                ProfilePicture = user.ProfilePicture,
                Roles = (user.Roles != null && user.Roles.Count > 0) ? user.Roles.Select(r => r.ToModel()).ToList() : null
            };

            return model;
        }

        public static UserModel ToSimpleModel(this User user)
        {
            var model = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                Active = user.Active,
                ProfilePicture = user.ProfilePicture
            };

            return model;
        }

        public static RoleModel ToModel(this Role role)
        {
            var model = new RoleModel
            {
                Id = role.Id,
                Title = role.Title
            };

            return model;
        }

        public static PageModel ToModel(this Page page)
        {
            var model = new PageModel
            {
                Id = page.Id,
                Title = page.Title,
                UrlId = page.UrlId,
                FbType = page.FbType,
                Url = page.Url,
                FbId = page.FbId,
                Importance = page.Importance
            };

            return model;
        }

        public static PageFeedModel ToModel(this PageFeed feed)
        {
            var model = new PageFeedModel
            {
                Id = feed.Id,
                Message = !string.IsNullOrEmpty(feed.Message) ? feed.Message : null,
                FbUpdatedTime = feed.FbUpdatedTime,
                PostPicture = feed.PostPicture,
                PostName = feed.PostName,
                Shares = feed.Shares,
                FbCreatedTime = feed.FbCreatedTime,
                Type = feed.Type,
                TimeCreaded = feed.TimeCreaded,
                TimeUpdated = feed.TimeUpdated,
                PostId = feed.PostId,
                PageId = feed.PageId,
                DateImported = feed.DateImported,
                Link = feed.Link,
                DateUsed = feed.DateUsed,
                IsUsed = feed.IsUsed
            };

            return model;
        }
    }
}
