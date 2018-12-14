using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.Network;
using EVSlideShow.Core.Network.Managers;
using EVSlideShow.Core.ViewModels.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.ViewModels {
    public class ImageCroppingViewModel : BaseViewModel {

        #region Variables

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

        public List<byte[]> EncodedBytes = new List<byte[]>();
        public List<byte[]> UpdatedEncodedBytes = new List<byte[]>();

        #endregion

        public ImageCroppingViewModel() {
           
        }


        public Image ImageFromByteArray(byte[] bytes) {
            return new Image { Source = ImageSource.FromStream(() => new MemoryStream(bytes)) };
        }

        public bool CanLoadNextImage() {
            ImageIndex++;
            return ImageIndex <= EncodedBytes.Count - 1;
        }

        public async Task<bool> SendImagesToServerAsync() {
            ImageNetworkManager manager = new ImageNetworkManager();
            return await manager.SendImages(this.User.AuthToken, this.SlideShowNumber, UpdatedEncodedBytes);
        }
    }
}

