using System;

namespace FbCollector.Models
{
    public class PageFeedModel
    {
        public int Id { get; set; }

        public string PostId { get; set; }

        public string Link { get; set; }

        public string PostPicture { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public string PostName { get; set; }

        public string FbCreatedTime { get; set; }

        public string FbUpdatedTime { get; set; }

        public DateTime TimeCreaded { get; set; }

        public DateTime TimeUpdated { get; set; }

        public int Shares { get; set; }

        public string PageId { get; set; }

        public DateTime DateImported { get; set; }

        public bool IsUsed { get; set; }

        public DateTime? DateUsed { get; set; }
    }
}
