using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;
using xamToolBarItem = Xamarin.Forms.ToolbarItem;

namespace EVSlideShow.Core.Views {
    public class ImageCroppingContentPage : BaseContentPage<ImageCroppingViewModel> {


        #region Variables
        private string testImagebase64;
        private Button _ButtonReset;
        private Button ButtonReset {
            get {
                if (_ButtonReset == null) {
                    _ButtonReset = new Button {
                        Text = "Reset",
                        FontSize = 18,
                        TextColor = Color.FromHex("618ec6"),
                        BackgroundColor = Color.White,
                        CornerRadius = 8,
                        HeightRequest = 50,
                        WidthRequest = 100,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Center

                    };
                    _ButtonReset.Clicked += ButtonReset_Clicked;
                    _ButtonReset.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }
                return _ButtonReset;
            }
        }
        private SfImageEditor _Editor;
        private SfImageEditor Editor {
            get {
                if (_Editor == null) {
                    _Editor = new SfImageEditor();
                    //_Editor.SetToolbarItemVisibility("Back, Text, Add, TextColor, FontFamily, Arial, Noteworthy, Marker Felt, Bradley Hand, SignPainter, Opacity, Path, StrokeThickness, Colors, " +
                    //"Opacity, Shape, Rectangle, StrokeThickness, Circle, Arrow, Transform, Crop, free, original, square, 3:1, 3:2, 4:3, 5:4, 16:9, Rotate, Flip, Undo, Redo,Save", false);
                    _Editor.ToolbarSettings.ToolbarItemSelected += ToolbarSettings_ToolbarItemSelected;
                    _Editor.ImageLoaded += Editor_ImageLoaded;
                    _Editor.ItemSelected += Editor_ItemSelected;
                    _Editor.EndReset += Editor_EndReset;
                    _Editor.ToolbarSettings.IsVisible = false;


                }

                return _Editor;
            }




        }

        private xamToolBarItem _ToolbarCropSave;
        private xamToolBarItem ToolbarCropSave {
            get {
                if (_ToolbarCropSave == null) {
                    _ToolbarCropSave = new xamToolBarItem("Crop", "", ToolbarCropSave_Pressed);
                }
                return _ToolbarCropSave;
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

            // toolbar item save
            ToolbarItems.Add(ToolbarCropSave);

            Grid grid = new Grid {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Star }, // row 0
                    new RowDefinition { Height = new GridLength(100, GridUnitType.Absolute) }, // row 1

                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) }, // col 0
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // col 1
                },

            };

            // grid.Children.Add(item ,col, col+colSpan, row, row+rowspan)
            grid.Children.Add(Editor, 0, 2, 0, 1);
            grid.Children.Add(ButtonReset, 0, 2, 1, 2);
            // TODO: ADD THE CROP/SAVE AS BUTTON

            this.Content = grid;

        }

        public static Image ImageFromBase64(string base64picture) {
            byte[] imageBytes = Convert.FromBase64String(base64picture); return new Image { Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)) };
        }

        #region Events
        void ButtonReset_Clicked(object sender, EventArgs e) {
            Console.WriteLine("RESET");
            Editor.Reset();
        }

        void ToolbarCropSave_Pressed() {
            if (this.ToolbarCropSave.Text == "Crop") {
                Editor.Crop();
                this.ToolbarCropSave.Text = "Save";
            } else if (this.ToolbarCropSave.Text == "Save") {
                DisplayAlert("Alert", "Save now", "ok");
            }
        }


        void Editor_EndReset(object sender, EndResetEventArgs args) {
            this.ToolbarCropSave.Text = "Crop";
            Editor.ToggleCropping(12, 7);
        }
        private void Editor_ImageLoaded(object sender, ImageLoadedEventArgs args) {
            Editor.ToggleCropping(12, 7);
        }

        private void ToolbarSettings_ToolbarItemSelected(object sender, ToolbarItemSelectedEventArgs e) {
            var item = e.ToolbarItem as Syncfusion.SfImageEditor.XForms.ToolbarItem;
            Console.WriteLine("Selected ToolbarItem is  " + e.ToolbarItem.Name);
        }

        private void Editor_ItemSelected(object sender, ItemSelectedEventArgs args) {
            var Settings = args.Settings;


        }
        #endregion

        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}

