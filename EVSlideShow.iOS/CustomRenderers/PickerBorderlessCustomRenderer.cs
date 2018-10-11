using System;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PickerBorderless), typeof(PickerBorderlessCustomRenderer))]
namespace EVSlideShow.iOS.CustomRenderers {
    public class PickerBorderlessCustomRenderer : PickerRenderer {
        public PickerBorderlessCustomRenderer() {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }

    }
}
