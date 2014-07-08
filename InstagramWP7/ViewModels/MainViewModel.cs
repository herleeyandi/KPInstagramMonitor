using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;


namespace InstagramWP
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

        }

        public ObservableCollection<UserViewModel> Following { get; private set; }
        public ObservableCollection<UserViewModel> Followers { get; private set; }


        public bool IsDataLoaded
        {
            get;
            private set;
        }


        public async void LoadData()
        {
            string accessToken = (string)IsolatedStorageSettings.ApplicationSettings["access_token"];

            InstagramAPI apiClient = new InstagramAPI(accessToken);
            Following = await apiClient.GetFollowing();
            NotifyPropertyChanged("Following");
            Followers =  await apiClient.GetFollowers();
            NotifyPropertyChanged("Followers");
            this.IsDataLoaded = true;
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