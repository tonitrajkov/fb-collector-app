using System;

namespace FbCollector.Domain
{
    public class PageFeed
    {
        private string _postId;
        private string _link;
        private string _postPicture;
        private string _message;
        private string _type;
        private string _postName;
        private string _fbCreatedTime;
        private string _fbUpdatedTime;
        private DateTime _timeCreaded;
        private DateTime _timeUpdated;
        private int _shares;
        private string _pageId;
        private DateTime _dateImported;
        private bool _isUsed;
        private DateTime? _dateUsed;

        #region Properties

        public virtual int Id { get; set; }

        public virtual string PostId
        {
            get { return _postId; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                if (value.Length > 150)
                    throw new ArgumentOutOfRangeException("value");

                _postId = value;
            }
        }

        public virtual string Link
        {
            get { return _link; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1000)
                    throw new ArgumentOutOfRangeException("value");

                _link = value;
            }
        }

        public virtual string PostPicture
        {
            get { return _postPicture; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1000)
                    throw new ArgumentOutOfRangeException("value");

                _postPicture = value;
            }
        }

        public virtual string Message
        {
            get { return _message; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1000000)
                    throw new ArgumentOutOfRangeException("value");

                _message = value;
            }
        }

        public virtual string Type
        {
            get { return _type; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 100)
                    throw new ArgumentOutOfRangeException("value");

                _type = value;
            }
        }

        public virtual string PostName
        {
            get { return _postName; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 500)
                    throw new ArgumentOutOfRangeException("value");

                _postName = value;
            }
        }

        public virtual string FbCreatedTime
        {
            get { return _fbCreatedTime; }
            set { _fbCreatedTime = value; }
        }

        public virtual string FbUpdatedTime
        {
            get { return _fbUpdatedTime; }
            set { _fbUpdatedTime = value; }
        }

        public virtual DateTime TimeCreaded
        {
            get { return _timeCreaded; }
            set { _timeCreaded = value; }
        }

        public virtual DateTime TimeUpdated
        {
            get { return _timeUpdated; }
            set { _timeUpdated = value; }
        }

        public virtual int Shares
        {
            get { return _shares; }
            set { _shares = value; }
        }

        public virtual string PageId
        {
            get { return _pageId; }
            set { _pageId = value; }
        }

        public virtual DateTime DateImported
        {
            get { return _dateImported; }
            set { _dateImported = value; }
        }

        public virtual bool IsUsed
        {
            get { return _isUsed; }
            set { _isUsed = value; }
        }

        public virtual DateTime? DateUsed
        {
            get { return _dateUsed; }
            set { _dateUsed = value; }
        }

        #endregion

        #region Constructors

        protected PageFeed()
        {
        }

        public PageFeed(string postId, string link, string type, string pageId)
        {
            _postId = postId;
            _link = link;
            _type = type;
            _pageId = pageId;
            _dateImported = DateTime.Now;
            _isUsed = false;
        }

        #endregion
    }
}
