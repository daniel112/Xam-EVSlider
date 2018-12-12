using System;
using System.IO;
using Android.Graphics;
using EVSlideShow.Core.Components.Common.DependencyInterface.Helpers;
using EVSlideShow.Droid.Common.DependencyImplementations;

[assembly: Xamarin.Forms.Dependency(typeof(ImageHelper))]
namespace EVSlideShow.Droid.Common.DependencyImplementations {
    public class ImageHelper : IImageHelper {
        public byte[] ResizeImage(byte[] imageData, float width, float height) {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream()) {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                return ms.ToArray();
            }
        }
    }
}
