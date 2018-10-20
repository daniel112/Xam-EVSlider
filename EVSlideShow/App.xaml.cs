using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EVSlideShow {
    public partial class App : Application {

        // initial values obtained from device specific startup
        public static double DisplayScreenWidth = 0f;
        public static double DisplayScreenHeight = 0f;
        public static double DisplayScaleFactor = 0f;
        public static DeviceOrientatione DeviceOrientation;

        public App() {
            InitializeComponent();
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU5OTRAMzEzNjJlMzMyZTMwbXdMRUxWYmhnVFR2QUpiMjdJWWJCNmFqSEcxOGc2R1BLbzI2ZG8rZkF5TT0=");
            MainPage = new SplashContentPage();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
