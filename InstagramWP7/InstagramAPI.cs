using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstagramWP
{
    public class InstagramAPI
    {
        private readonly string apiBaseUrl = "https://api.instagram.com/v1";
        private readonly string apiAccessToken;

        private HttpClient _httpClient;

        public InstagramAPI(string accessToken)
        {
            this.apiAccessToken = accessToken;
            _httpClient = new HttpClient();
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage request) where T : class
        {

            HttpResponseMessage response;
            response = await _httpClient.SendAsync(request);

            //TODO: Error Handling
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseBody);
            }

            return null;
        }

        public async Task<ObservableCollection<UserViewModel>> GetFollowers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiBaseUrl + "/users/self/followed-by?access_token=" + apiAccessToken);
            var followersResponse =  await SendAsync<InstagramResponse>(request);
            var result = new ObservableCollection<UserViewModel>();
            foreach (var u in followersResponse.data)
            {
                result.Add(new UserViewModel() { FullName = u.full_name, Username = u.username, ProfilePicUrl = u.profile_picture });
            }
            return result;
        }

        public async Task<ObservableCollection<UserViewModel>> GetFollowing()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiBaseUrl + "/users/self/follows?access_token=" + apiAccessToken);
            var followingResponse = await SendAsync<InstagramResponse>(request);
            var result = new ObservableCollection<UserViewModel>();
            foreach (var u in followingResponse.data)
            {
                result.Add(new UserViewModel() { FullName = u.full_name, Username = u.username, ProfilePicUrl = u.profile_picture });
            }
            return result;
        }
    }

    public class InstagramUser
    {
        public string username { get; set; }
        public string bio { get; set; }
        public string website { get; set; }
        public string profile_picture { get; set; }
        public string full_name { get; set; }
        public string id { get; set; }
    }

    public class InstagramResponse
    {
        public List<InstagramUser> data { get; set; }
    }
}
