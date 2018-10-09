using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace EVSlideShow.Droid {
    [Activity(
              Theme = "@style/MainTheme", MainLauncher = false,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            App.DisplayScreenWidth = (double)Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density;
            App.DisplayScreenHeight = (double)Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density;
            App.DisplayScaleFactor = (double)Resources.DisplayMetrics.Density;

            LoadApplication(new App());
        }
    }
}