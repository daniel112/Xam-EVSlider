using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.Helpers;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.Views;
using EVSlideShow.Views;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk3MDNAMzEzNjJlMzQyZTMwV1V1UHBKOWF3MlMyYU1qdHJWcFRlRHhKc0JSMkIzTmY4bmh0bTM3elpUZz0=");
            if (Application.Current.Properties.ContainsKey("User")) {
                try {

                    var user = ObjectSerializerHelper.Convertbase64StringToObject<User>((string)Application.Current.Properties["User"]);
                    MainPage = new BaseNavigationPage(new ManageImageFileContentPage(user));
                } catch {
                    MainPage = new SplashContentPage();
                }

            } else {
                MainPage = new SplashContentPage();
            }

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
