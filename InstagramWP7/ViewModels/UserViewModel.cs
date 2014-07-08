using System;
using System.ComponentModel;

namespace InstagramWP
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }

        private string _fullName;
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (value != _fullName)
                {
                    _fullName = value;
                    NotifyPropertyChanged("FullName");
                }
            }
        }

        private string _profilePicUrl;
        public string ProfilePicUrl
        {
            get
            {
                return _profilePicUrl;
            }
            set
            {
                if (value != _profilePicUrl)
                {
                    _profilePicUrl = value;
                    NotifyPropertyChanged("ProfilePicUrl");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}