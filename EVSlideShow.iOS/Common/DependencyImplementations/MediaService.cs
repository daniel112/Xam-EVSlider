using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using EVSlideShow.Core.Components.Common.DependencyInterface;
using EVSlideShow.Core.Constants;
using EVSlideShow.iOS.Common.DependencyImplementations;
using Foundation;
using GMImagePicker;
using Photos;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace EVSlideShow.iOS.Common.DependencyImplementations {
    public class MediaService : IMediaService {

        private GMImagePickerController _PickerController;
        private GMImagePickerController PickerController {
            get {
                if (_PickerController == null) {
                    _PickerController = new GMImagePickerController {
                        GridSortOrder = SortOrder.Descending,
                        Title = "Select Up To 5 Images",
                        DisplaySelectionInfoToolbar = true,
                    };
                }
                _PickerController.ShouldSelectAsset += PickerController_ShouldSelectAsset;
                _PickerController.FinishedPickingAssets += PickerController_FinishedPickingAssets;
                _PickerController.Canceled += (object sender, EventArgs e) => {
                    Console.WriteLine("user canceled picking assets");
                };
                return _PickerController;

            }



        }

        public MediaService() {
        }

        public void OpenGallery() {
            _PickerController = null;
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(PickerController, true, null);

        }

        #region Delegates
        void PickerController_ShouldSelectAsset(object sender, CancellableAssetEventArgs args) {

            var controller = (GMImagePickerController)sender;
            args.Cancel = controller.SelectedAssets.Count >= 5;

        }
        void PickerController_FinishedPickingAssets(object sender, MultiAssetEventArgs args) {
            List<object> encodedImages = new List<object>();
            foreach (var asset in args.Assets) {
                PHImageManager imageManager = new PHImageManager();

                // we make sure the image are saved in the same quality with this option
                PHImageRequestOptions options = new PHImageRequestOptions {
                    NetworkAccessAllowed = true,
                    DeliveryMode = PHImageRequestOptionsDeliveryMode.HighQualityFormat,
                    Synchronous = true
                };
                imageManager.RequestImageForAsset(asset,
                    new CGSize(asset.PixelWidth, asset.PixelHeight),PHImageContentMode.Default, options,
                    (image, info) => {
                        // TODO: testing quality
                        byte[] dataBytes = new byte[image.AsPNG().Length];
                        System.Runtime.InteropServices.Marshal.Copy(image.AsPNG().Bytes, dataBytes, 0, Convert.ToInt32(image.AsPNG().Length));
                        encodedImages.Add(dataBytes);
                    });
            }
            // post the message with the list attached
            MessagingCenter.Send<object, object>(this, MessagingKeys.DidFinishSelectingImages, encodedImages);

        }


        #endregion
    }
}
