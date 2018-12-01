using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EVSlideShow.Core.Network.Managers {
    public class ImageNetworkManager : BaseClient {
        #region Variables
        private const string baseURL = "http://www.evslideshow.com/";

        #endregion

        #region Initialization

        #endregion

        #region Private API

        #endregion

        #region Public API
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

        public async Task<bool> DeletePhotosByID(string ids, int slideshowNum, string userAuth) {
        
            var method = $"/delete_images?order_ids={ids}&slideshow_number={slideshowNum}";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));

            Client.DefaultRequestHeaders.Add("Authorization", userAuth);

            var response = await Client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode) {
                return true;
            } else {
                return false;
            }

        }

        public async Task<bool> DeleteAll(int slideshowNum, string userAuth) {

            var method = $"delete_images?all=true&slideshow_number={slideshowNum}";
            var uri = new Uri(string.Format(baseURL + method, string.Empty));

            Client.DefaultRequestHeaders.Add("Authorization", userAuth);

            var response = await Client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode) {
                return true;
            } else {
                return false;
            }

        }
        #endregion

        #region Delegates

        #endregion
    }
}
