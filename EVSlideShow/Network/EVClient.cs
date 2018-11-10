using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EVSlideShow.Core.Models;
using Newtonsoft.Json;

namespace EVSlideShow.Core.Network {
    public class EVClient : BaseClient {

        #region Variables
        private const string baseURL = "http://www.evslideshow.com/";
        #endregion

        #region Initialization
        public EVClient() {
        }
        #endregion

        #region Private API

        #endregion

        #region Public API

        public async Task<User> RegisterUser(User user) {
            User output = new User();
            var method = "users";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));
            var values = new Dictionary<string, string>
            {
                { "password",user.Password },
                { "username", user.Username },
                { "email", user.Email },
                { "ev_type", user.EVType },

            };
            // serialize dict into json string
            string json = JsonConvert.SerializeObject(values);


            var response = await Client.PostAsync(uri, new StringContent(json));
            if (response.IsSuccessStatusCode) {
                var jsonResult = await response.Content.ReadAsStringAsync();
                output = JsonConvert.DeserializeObject<User>(jsonResult);
                Console.WriteLine("");
            } else if (response.StatusCode == (HttpStatusCode)422) {
                var rawResponse = await response.Content.ReadAsStringAsync();
                output.Message = rawResponse;
            }

            return output;
        }

        public async Task<User> LoginAsync(string username, string password) {
            User user = new User();
            var method = "authentication";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));
            var values = new Dictionary<string, string>
            {
               { "password",password },
               { "username", username },

            };

            // serialize dict into json string
            string json = JsonConvert.SerializeObject(values);
            var response = await Client.PostAsync(uri, new StringContent(json));

            if (response.IsSuccessStatusCode) {
                var jsonResult = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(jsonResult);
                user.Success = true;
            } else {
                var jsonResult = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(jsonResult);
                user.Message = "Network error, please try again later";
                user.Success = false;
            }

            return user;
        }

        public async Task<List<User>> GetAllUsers() {
            var getResponse = await Client.GetAsync(new Uri(string.Format(baseURL + "users", string.Empty)));
            var users = new List<User>();
            if (getResponse.IsSuccessStatusCode) {
                var jsonResult = await getResponse.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(jsonResult);
            }
            return users;
        }

        public async Task<bool> SendEmailForRecovery(string email) {
            // /password_recovery?email=
            var getResponse = await Client.GetAsync(new Uri(string.Format(baseURL + $"password_recovery?email={email}", string.Empty)));
            return getResponse.IsSuccessStatusCode;
        }


        public async Task<bool> SendImages(string userAuth, int slideshowNum, List<byte[]> imageDatas) {

            var method = $"images_upload_v2?slideshow_number={slideshowNum}";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));

            MultipartFormDataContent form = new MultipartFormDataContent();
            for (int i = 0; i <= imageDatas.Count - 1; i++) {
                string fileName = $"image{i}.jpg";
                var imageContent = new ByteArrayContent(imageDatas[i], 0, imageDatas[i].Length);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                form.Add(imageContent, "images[]", fileName);
            }

                
            Client.DefaultRequestHeaders.Add("Authorization", userAuth);

            var response = await Client.PostAsync(uri, form);

            if (response.IsSuccessStatusCode) {
                var jsonResult = await response.Content.ReadAsStringAsync();
                Console.WriteLine("SUCCESS");
                return true;
            } else {
                var jsonResult = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"ERROR: {jsonResult}");
                return false;
            }
        }

        #endregion

        #region Delegates

        #endregion
    }
}
