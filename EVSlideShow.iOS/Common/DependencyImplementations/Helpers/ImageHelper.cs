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
            UIImageOrientation orientation = originalImage.Orientation;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst)) {

                RectangleF imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIImage resizedImage = UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
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
