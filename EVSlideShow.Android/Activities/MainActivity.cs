using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using Android.Content;
using System.Collections.Generic;
using Android.Database;
using Android.Provider;
using System.IO;
using Android.Graphics;
using Xamarin.Forms;
using EVSlideShow.Core.Constants;
using Android.Media;
using EVSlideShow.Droid.Common.Helpers;
using System.Threading.Tasks;
using Plugin.InAppBilling;

namespace EVSlideShow.Droid {
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        public static int OPENGALLERYCODE = 100;

        #region UILifeCycle
        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            // current activity plugin

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            App.DisplayScreenWidth = (double)Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density;
            App.DisplayScreenHeight = (double)Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density;
            App.DisplayScaleFactor = (double)Resources.DisplayMetrics.Density;

            LoadApplication(new App());
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            base.OnActivityResult(requestCode, resultCode, data);
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);

            if (requestCode == OPENGALLERYCODE && resultCode == Result.Ok) {

                List<string> images = new List<string>();

                if (data != null) {
                    ClipData clipData = data.ClipData;
                    // if clipData exists, it means there are more than 1 image

                    if (clipData != null) {
                        // we need to send this message so the view can show loading
                        MessagingCenter.Send<object>(this, MessagingKeys.ShowLoadingIndicator);

                        for (int i = 0; i < clipData.ItemCount; i++) {
                            if (i > 5) { break; } // limit to 5 images
                            try {
                                ClipData.Item item = clipData.GetItemAt(i);
                                var uri = item.Uri;

                                System.IO.Stream stream = ContentResolver.OpenInputStream(uri);

                                await Task.Run(() => {                              
                                    images.Add(PhotoUtilHelper.UpdateAndConvertURI(this, uri));
                                });

                            } catch (Exception ex) {
                                Console.WriteLine(ex.Message);
                            }

                        }
                    } else {

                        try {
                            var uri = data.Data;
                            System.IO.Stream stream = ContentResolver.OpenInputStream(uri);

                            await Task.Run(() => {
                                images.Add(PhotoUtilHelper.UpdateAndConvertURI(this, uri));
                            });
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }

                    }

                    // post the message with the list attached
                    MessagingCenter.Send<object, object>(this, MessagingKeys.DidFinishSelectingImages, images);
                }
            }
        }


        #endregion

        #region Private API

        #endregion


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}