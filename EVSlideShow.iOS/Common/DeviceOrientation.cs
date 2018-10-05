using EVSlideShow.Core.Common;
using EVSlideShow.iOS.Common;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceOrientation))]
namespace EVSlideShow.iOS.Common {
    public class DeviceOrientation : IDeviceOrientation {
        public DeviceOrientation() { }

        public DeviceOrientatione GetOrientation() {
            var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
            bool isPortrait = currentOrientation == UIInterfaceOrientation.Portrait || currentOrientation == UIInterfaceOrientation.PortraitUpsideDown;

            return isPortrait ? DeviceOrientatione.Portrait : DeviceOrientatione.Landscape;
        }
    }
}
