using System;
using System.Security.Permissions;
using Android.App;
using Android.Content;
using Android.Widget;
using EVSlideShow.Core.Components.Common.DependencyInterface;
using EVSlideShow.Droid.Common.DependencyImplementations;
using Plugin.CurrentActivity;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace EVSlideShow.Droid.Common.DependencyImplementations {
    public class MediaService : Java.Lang.Object, IMediaService {


        public void OpenGallery() {

            Toast.MakeText(CrossCurrentActivity.Current.AppContext, "Select up to 5 image(s)", ToastLength.Long).Show();
            var imageIntent = new Intent(
                Intent.ActionPick);
            imageIntent.SetType("image/*");
            imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            imageIntent.SetAction(Intent.ActionGetContent);
            CrossCurrentActivity.Current.Activity.StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), MainActivity.OPENGALLERYCODE);

        }
    }
}

