using System;
using System.Collections.Generic;
using System.IO;
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
        public int ImageIndex = 0;
        #endregion

        public ImageCroppingViewModel() {
           
        }

        public Image ImageFromBase64(string base64picture) {
            byte[] imageBytes = Convert.FromBase64String(base64picture); return new Image { Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)) };
        }

        public bool CanLoadNextImage() {
            ImageIndex++;
            return ImageIndex <= EncodedImages.Count - 1;
        }

        public bool SendImagesToServer() {
            EVClient client = new EVClient();

            return false;

        }
    }
}

