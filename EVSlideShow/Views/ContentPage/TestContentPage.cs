using System;
using System.Reflection;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class TestContentPage : ContentPage {

        private SfImageEditor _Editor;
        private SfImageEditor Editor {
            get {
                if (_Editor == null) {
                    _Editor = new SfImageEditor();

                }

                return _Editor;
            }
        }
        public TestContentPage() {

            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            Editor.Source = ImageSource.FromResource("EVSlideShow.Core.image.jpeg", assembly);
            Editor.ImageLoaded += Editor_ImageLoaded;

            Content = Editor;
        }

        private void Editor_ImageLoaded(object sender, ImageLoadedEventArgs args) {
            Editor.ToggleCropping(12, 7);
        }
    }
}

