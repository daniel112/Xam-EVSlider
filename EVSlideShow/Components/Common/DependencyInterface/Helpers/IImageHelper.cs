using System;
namespace EVSlideShow.Core.Components.Common.DependencyInterface.Helpers {
    public interface IImageHelper {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
