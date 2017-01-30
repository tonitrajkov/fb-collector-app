using System;

namespace FbCollector.Domain
{
    public class Page
    {
        private string _title;
        private string _url;
        private string _urlId;
        private string _fbId;
        private string _fbType;
        private DateTime _dateCreated;

        #region Properties

        public virtual int Id { get; set; }

        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                if (value.Length > 250)
                    throw new ArgumentOutOfRangeException("value");

                _title = value;
            }
        }

        public virtual string Url
        {
            get { return _url; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 250)
                    throw new ArgumentOutOfRangeException("value");

                _url = value;
            }
        }

        public virtual string UrlId
        {
            get { return _urlId; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 100)
                    throw new ArgumentOutOfRangeException("value");

                _urlId = value;
            }
        }

        public virtual string FbId
        {
            get { return _fbId; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 100)
                    throw new ArgumentOutOfRangeException("value");

                _fbId = value;
            }
        }

        public virtual string FbType
        {
            get { return _fbType; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 100)
                    throw new ArgumentOutOfRangeException("value");

                _fbType = value;
            }
        }

        public virtual DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        #endregion

        #region Constructors

        protected Page()
        {
        }

        public Page(string title, string url, string urlId)
        {
            _title = title;
            _url = url;
            _urlId = urlId;
            _dateCreated = DateTime.Now;
        }

        #endregion
    }
}
