using System;
using System.Drawing;
using CoreGraphics;
using EVSlideShow.Core.Components.Common.DependencyInterface.Helpers;
using EVSlideShow.iOS.Common.DependencyImplementations.Helpers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ImageHelper))]
namespace EVSlideShow.iOS.Common.DependencyImplementations.Helpers {
    public class ImageHelper : IImageHelper {

        public byte[] ResizeImage(byte[] imageData, float width, float height) {
            UIImage originalImage = ImageFromByteArray(imageData);

            var originalHeight = originalImage.Size.Height;
            var originalWidth = originalImage.Size.Width;

            nfloat newHeight = 0;
            nfloat newWidth = 0;

            if (originalHeight > originalWidth) {
                newHeight = height;
                nfloat ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            } else {
                newWidth = width;
                nfloat ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            width = (float)newWidth;
            height = (float)newHeight;

            UIGraphics.BeginImageContext(new SizeF(width, height));
            originalImage.Draw(new RectangleF(0, 0, width, height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            var bytesImagen = resizedImage.AsPNG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }

        private UIImage ImageFromByteArray(byte[] data) {
            if (data == null) {
                return null;
            }

            UIImage image;
            try {
                image = new UIImage(Foundation.NSData.FromArray(data));
            } catch (Exception e) {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
