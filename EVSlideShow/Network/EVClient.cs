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
        public async Task<bool> LoginAsync(string username, string password) {

            var method = "authentication";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));
            var values = new Dictionary<string, string>
            {
               { "username", username },
               { "password",password }
            };
            // serialize dict into json string
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode) {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(jsonResult);

                return true;
            }
            return false;
        }
        #endregion

        #region Delegates

        #endregion
    }
}
