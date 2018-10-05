using System;
using Android.Content;
using Android.Runtime;
using Android.Hardware;
using Android.Views;
using EVSlideShow.Core.Common;
using EVSlideShow.Droid.Common;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceOrientation))]
namespace EVSlideShow.Droid.Common {
    public class DeviceOrientation : IDeviceOrientation {
        
        public DeviceOrientation() {
        }

        public static void Init() { }

        public DeviceOrientatione GetOrientation() {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandscape ? DeviceOrientatione.Landscape : DeviceOrientatione.Portrait;
        }
    }
}
