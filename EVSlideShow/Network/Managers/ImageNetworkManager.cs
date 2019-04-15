using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EVSlideShow.Core.Models;

namespace EVSlideShow.Core.Network.Managers {
    public class ImageNetworkManager : BaseClient {
        #region Variables
        private const string baseURL = "https://www.evslideshow.com/";

        #endregion

        #region Initialization

        #endregion

        #region Private API

        #endregion

        #region Public API
        public async Task<bool> SendImages(string userAuth, int slideshowNum, List<byte[]> imageDatas) {

            var method = $"/images_upload?slideshow_number={slideshowNum}";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));

            MultipartFormDataContent form = new MultipartFormDataContent();
            for (int i = 0; i <= imageDatas.Count - 1; i++) {
                string fileName = $"image{i}.jpg";
                var imageContent = new ByteArrayContent(imageDatas[i], 0, imageDatas[i].Length);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                form.Add(imageContent, "images[]", fileName);
            }

            try {
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
            } catch(Exception ex) {
                Console.WriteLine($"ERROR: {ex.Message}");
                return false;
            }

        }

        public async Task<NetworkDebug> DeletePhotosByID(string ids, int slideshowNum, string userAuth) {
        
            var method = $"/delete_images?order_ids={ids}&slideshow_number={slideshowNum}";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));

            Client.DefaultRequestHeaders.Add("Authorization", userAuth);

            try {
                var response = await Client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return new NetworkDebug(true, jsonResult, response.StatusCode);
                    //return true;
                } else {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return new NetworkDebug(false, jsonResult, response.StatusCode);
                    //return false;
                }
            } catch(Exception ex) {
                return new NetworkDebug(false, ex.Message, null);
            }


        }

        public async Task<NetworkDebug> DeleteAll(int slideshowNum, string userAuth) {

            var method = $"delete_images?all=true&slideshow_number={slideshowNum}";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));

            Client.DefaultRequestHeaders.Add("Authorization", userAuth);

            try {
                var response = await Client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return new NetworkDebug(true, jsonResult, response.StatusCode);
                    //return true;
                } else {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return new NetworkDebug(false, jsonResult, response.StatusCode);
                    //return false;
                }
            } catch(Exception ex) {
                return new NetworkDebug(false, ex.Message, null);
            }


        }
        #endregion

        #region Delegates

        #endregion
    }
}
