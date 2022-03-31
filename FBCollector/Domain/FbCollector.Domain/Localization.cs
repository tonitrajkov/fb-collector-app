using System;

namespace FbCollector.Domain
{
    public class Localization
    {
        private string _key;
        private string _value;
        private string _languageCode;

        #region Properties

        public virtual int Id { get; set; }

        public virtual string Key
        {
            get { return _key; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1000)
                    throw new ArgumentOutOfRangeException("value");

                _key = value;
            }
        }

        public virtual string Value
        {
            get { return _value; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1000000)
                    throw new ArgumentOutOfRangeException("value");

                _value = value;
            }
        }

        public virtual string LanguageCode
        {
            get { return _languageCode; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 10)
                    throw new ArgumentOutOfRangeException("value");

                _languageCode = value;
            }
        }

        #endregion

        #region Constructors

        protected Localization()
        {
        }

        public Localization(string key, string value, string code)
        {
            _key = key;
            _languageCode = code;
            _value = value;
        }

        #endregion
    }
}
