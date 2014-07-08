using Microsoft.Phone.Controls;
using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Navigation;

namespace InstagramWP
{
    public partial class Authentication : PhoneApplicationPage
    {
        public Authentication()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AuthBrowser.Navigate(new Uri("https://instagram.com/oauth/authorize/?client_id=bb5c1dc1010b4eddb396c2d41792bfdb&redirect_uri=http://instagram.com&response_type=token"));
        }

        void AuthBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            //access token is a Url fragment and these fragments start with '#'
            if (e.Uri.AbsoluteUri.Contains('#'))
            {
                //parse our access token
                if (e.Uri.Fragment.StartsWith("#access_token="))
                {
                    string token = e.Uri.Fragment.Replace("#access_token=", string.Empty);

                    //save our token
                    IsolatedStorageSettings.ApplicationSettings["access_token"] = token;
                    IsolatedStorageSettings.ApplicationSettings.Save();

                    //now that we have our token, let's go back to the MainPage
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                }
            }
        }
    }
}