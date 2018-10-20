using System;
using System.Collections.Generic;
using System.IO;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class ImageCroppingContentPage : BaseContentPage<ImageCroppingViewModel> {


        #region Variables
        private string testImagebase64;

        private SfImageEditor _Editor;
        private SfImageEditor Editor {
            get {
                if (_Editor == null) {
                    _Editor = new SfImageEditor();
                }
                _Editor.SetToolbarItemVisibility("Text, Add, TextColor, FontFamily, Arial, Noteworthy, Marker Felt, Bradley Hand, SignPainter, Opacity, Path, StrokeThickness, Colors, Opacity, Shape, Rectangle, StrokeThickness, Circle, Arrow," +
                                                 "free, original, square, Rotate, 3:1, 3:2, 4:3, 5:4,9:16, 16:9, Undo, Redo", false);
                _Editor.ToolbarSettings.ToolbarItemSelected += ToolbarSettings_ToolbarItemSelected;
                _Editor.ImageLoaded += Editor_ImageLoaded;
                return _Editor;
            }
        }



        #endregion

        #region Initialization
        public ImageCroppingContentPage() {
            this.Setup();

        }
        public ImageCroppingContentPage(List<string> encodedImages) {
            this.testImagebase64 = encodedImages[0];
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
        #endregion

        #region Private API
        private void Setup() {

            this.Title = "Crop Image";

            Editor.Source = ImageFromBase64(testImagebase64).Source;
            var toolbarItem = ((Editor.ToolbarSettings.ToolbarItems[3]) as FooterToolbarItem).SubItems[0] as FooterToolbarItem;
            toolbarItem.SubItems.Add(new Syncfusion.SfImageEditor.XForms.ToolbarItem() {
                Text = "12:7",
                Icon = ImageSource.FromResource("CustomCroppingSample.CropAspect.png")
            });


            this.Content = Editor;

        }

        public static Image ImageFromBase64(string base64picture) {
            byte[] imageBytes = Convert.FromBase64String(base64picture); return new Image { Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)) };
        }

        private void Editor_ImageLoaded(object sender, ImageLoadedEventArgs args) {
            // TODO: FIX issue with crop editor not movable unless touched
            Editor.ToggleCropping(12, 7);
        }

        private void ToolbarSettings_ToolbarItemSelected(object sender, ToolbarItemSelectedEventArgs e) {
            var item = e.ToolbarItem as Syncfusion.SfImageEditor.XForms.ToolbarItem;
            if (item.Text == "12:7") {
                Editor.ToggleCropping(12, 7);
            }
        }
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}

