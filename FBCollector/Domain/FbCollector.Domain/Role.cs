using System;

namespace FbCollector.Domain
{
    public class Role
    {
        private string _title;

        #region Properties

        public virtual int Id { get; set; }

        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 255)
                    throw new ArgumentOutOfRangeException("value");

                _title = value;
            }
        }

        #endregion

        #region Constructors

        protected Role()
        {
        }

        public Role(string title)
        {
            _title = title;
        }

        #endregion
    }
}
