using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.Network;
using EVSlideShow.Core.ViewModels.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.ViewModels {
    public class ImageCroppingViewModel : BaseViewModel {

        #region Variables
        private List<string> _EncodedImages;
        public List<string> EncodedImages {
            get {
                if (_EncodedImages == null) {
                    _EncodedImages = new List<string>();
                }
                return _EncodedImages;
            }
            set {
                _EncodedImages = value;
            }
        }
        public List<string> _UpdatedEncodedImages;
        public List<string> UpdatedEncodedImages {
            get {
                if (_UpdatedEncodedImages == null) {
                    _UpdatedEncodedImages = new List<string>();
                }
                return _UpdatedEncodedImages;
            }
        }

        public User _User;
        public User User {
            get {
                if (_User == null) {
                    _User = new User();
                }
                return _User;
            }
            set {
                _User = value;
            }
        }
        public int ImageIndex = 0;
        public int SlideShowNumber = 1;


        #endregion

        public ImageCroppingViewModel() {
           
        }

        private List<byte[]> ConvertListToByte() {
            List<byte[]> result = new List<byte[]>();
            foreach (var value in UpdatedEncodedImages) {
                result.Add(Convert.FromBase64String(value));
            }
            return result;
        }


        public Image ImageFromBase64(string base64picture) {
            byte[] imageBytes = Convert.FromBase64String(base64picture); return new Image { Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)) };
        }

        public bool CanLoadNextImage() {
            ImageIndex++;
            return ImageIndex <= EncodedImages.Count - 1;
        }

        public async Task<bool> SendImagesToServerAsync() {
            // turn the list into byte[]
            EVClient client = new EVClient();
            return await client.SendImages(this.User.AuthToken, this.SlideShowNumber, ConvertListToByte());

        }
    }
}

