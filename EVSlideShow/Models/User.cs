using System;
using Newtonsoft.Json;

namespace EVSlideShow.Core.Models {

    public static class EVTypeName {
        public const string TeslaModelS = "Tesla Model S";
        public const string TeslaModelX = "Tesla Model X";
        public const string TeslaModel3 = "Tesla Model 3";

    }

    [Serializable]
    public class User : BaseModel {

        #region Variables
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ev_type")]
        public string EVType { get; set; }

        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime ?UpdatedAt { get; set; }

        [JsonProperty("is_subscribe")]
        public bool IsSubscribed { get; set; }

        [JsonProperty("is_multiple_slideshow_subscribe")]
        public bool HasMultipleSubscription { get; set; }

        [JsonProperty("subscription_url")]
        public string SubscriptionURL { get; set; }

 
        #endregion

        #region Initialization
        public User() {
        }
        #endregion

        #region Private API

        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
