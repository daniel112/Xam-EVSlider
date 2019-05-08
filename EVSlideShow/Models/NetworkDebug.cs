using System;
using System.Net;

namespace EVSlideShow.Core.Models {
    public class NetworkDebug {
        public bool Success;
        public string Message;
        public HttpStatusCode? StatusCode;

        public NetworkDebug() {
        }

        public NetworkDebug(bool success, string message, HttpStatusCode? statuscode) {
            Success = success;
            Message = message;
            StatusCode = statuscode;
        }
    }
}
