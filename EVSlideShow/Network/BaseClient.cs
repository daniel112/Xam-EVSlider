using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EVSlideShow.Core.Network {
    public class BaseClient {
        #region Variables
        private HttpClient _Client;
        protected HttpClient Client {
            get {
                if (_Client == null) {
                    _Client = new HttpClient {
                        MaxResponseContentBufferSize = 256000,
                    };
                    _Client.DefaultRequestHeaders
                           .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                }
                return _Client;
            }
        }
        #endregion

        #region Initialization
        public BaseClient() {
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
