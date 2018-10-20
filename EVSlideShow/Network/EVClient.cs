using System;
using System.Collections.Generic;
using System.Net.Http;
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

           // var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(uri, new StringContent(json));

            if (response.IsSuccessStatusCode) {
                var jsonResult = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(jsonResult);
                user.Success = true;
            } else {
                var jsonResult = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(jsonResult);
                user.Success = false;
            }



            // TODO: TESTING GET
            var getResponse = await Client.GetAsync(new Uri(string.Format(baseURL + "users", string.Empty)));
            if (getResponse.IsSuccessStatusCode) {
                var jsonResult = await getResponse.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(jsonResult);
            }

            return user;
        }
        #endregion

        #region Delegates

        #endregion
    }
}
