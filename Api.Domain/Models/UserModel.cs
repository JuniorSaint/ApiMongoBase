using System;
namespace Api.Domain.Models
{
    public class UserModel : BaseModel
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _isActive;
        public string IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private string _userType;
        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
