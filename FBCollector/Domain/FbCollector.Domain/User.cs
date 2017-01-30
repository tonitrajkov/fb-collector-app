using System;
using System.Collections.Generic;

namespace FbCollector.Domain
{
    public class User
    {
        private readonly string _userName;
        private string _fullName;
        private string _email;
        private string _address;
        private string _city;
        private string _state;
        private string _telephone;
        private bool _active;
        private DateTime _dateCreated;
        private DateTime _dateModified;

        #region Properties

        public virtual int Id { get; set; }

        public virtual string UserName
        {
            get { return _userName; }
        }

        public virtual string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                if (value.Length > 255)
                    throw new ArgumentOutOfRangeException("value");

                _fullName = value;
            }
        }

        public virtual string Email
        {
            get { return _email; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 255)
                    throw new ArgumentOutOfRangeException("value");

                _email = value;
            }
        }

        public virtual byte[] Password { get; set; }

        public virtual string ProfilePicture { get; set; }

        public virtual bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public virtual string Address
        {
            get { return _address; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 255)
                    throw new ArgumentOutOfRangeException("value");

                _address = value;
            }
        }

        public virtual string City
        {
            get { return _city; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 255)
                    throw new ArgumentOutOfRangeException("value");

                _city = value;
            }
        }

        public virtual string State
        {
            get { return _state; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 255)
                    throw new ArgumentOutOfRangeException("value");

                _state = value;
            }
        }

        public virtual string Telephone
        {
            get { return _telephone; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 150)
                    throw new ArgumentOutOfRangeException("value");

                _telephone = value;
            }
        }

        public virtual DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        public virtual DateTime DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }

        public virtual string ChangePasswordToken { get; set; }

        public virtual DateTime? TokenExpireTime { get; set; }

        public virtual IList<Role> Roles { get; set; }

        #endregion

        #region Constructors

        protected User()
        {
        }

        public User(string fullname, string userName, string email, bool active)
        {
            _fullName = fullname;
            _userName = userName;
            _email = email;
            _active = active;
            _dateCreated = DateTime.Now;
            _dateModified = DateTime.Now;
        }

        #endregion
    }
}
