using System;

namespace FbCollector.Domain
{
    public class Language
    {
        private string _title;
        private string _code;
        private bool _isDefault;

        #region Properties

        public virtual int Id { get; set; }

        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 250)
                    throw new ArgumentOutOfRangeException("value");

                _title = value;
            }
        }

        public virtual string Code
        {
            get { return _code; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 10)
                    throw new ArgumentOutOfRangeException("value");

                _code = value;
            }
        }

        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        #endregion

        #region Constructors

        protected Language()
        {
        }

        public Language(string title, string code)
        {
            _title = title;
            _code = code;
            _isDefault = false;
        }

        #endregion
    }
}
