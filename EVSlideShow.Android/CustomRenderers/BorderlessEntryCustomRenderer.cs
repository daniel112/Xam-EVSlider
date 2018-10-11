using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryBorderless), typeof(BorderlessEntryCustomRenderer))]
namespace EVSlideShow.Droid.CustomRenderers {
    public class BorderlessEntryCustomRenderer : EntryRenderer {

        public BorderlessEntryCustomRenderer(Context context) : base(context) {


        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);
            if (e.OldElement == null) {
                Control.Background = null;
            }
        }


    }



}
