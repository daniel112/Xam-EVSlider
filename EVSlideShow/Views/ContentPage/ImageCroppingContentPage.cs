using System;
using System.Collections.Generic;
using System.IO;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class ImageCroppingContentPage : BaseContentPage<ImageCroppingViewModel> {


        #region Variables
        private Image _ImageContentManage;
        private Image ImageContentManage {
            get {
                if (_ImageContentManage == null) {
                    _ImageContentManage = new Image {
                        Aspect = Aspect.AspectFit,
                    };
                }
                return _ImageContentManage;
            }
        }
        private string testImagebase64;
        #endregion

        #region Initialization
        public ImageCroppingContentPage() {
            this.Setup();

        }
        public ImageCroppingContentPage(List<string>encodedImages) {
            this.testImagebase64 = encodedImages[0];
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
        #endregion

        #region Private API
        private void Setup() {

            //ImageContentManage.Source = ImageFromBase64(testImagebase64).Source;
            Content = new StackLayout {
                Children = {
                    ImageFromBase64(testImagebase64)
                }
            };
        }

        public static Image ImageFromBase64(string base64picture) { 
            byte[] imageBytes = Convert.FromBase64String(base64picture); return new Image { Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)) }; 
        }
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}

