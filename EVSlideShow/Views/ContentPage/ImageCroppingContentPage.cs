using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.Common.DependencyInterface.Helpers;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using EVSlideShow.Core.Views.ContentViews;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;
using xamToolBarItem = Xamarin.Forms.ToolbarItem;

namespace EVSlideShow.Core.Views {
    public class ImageCroppingContentPage : BaseContentPage<ImageCroppingViewModel> {


        #region Variables
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
        private Button _ButtonCropSave;
        private Button ButtonCropSave {
            get {
                if (_ButtonCropSave == null) {
                    _ButtonCropSave = new Button {
                        Text = "Crop",
                        FontSize = 18,
                        TextColor = Color.FromHex("618ec6"),
                        BackgroundColor = Color.White,
                        CornerRadius = 8,
                        HeightRequest = 50,
                        WidthRequest = 100,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Center

                    };
                    _ButtonCropSave.Clicked += ButtonCropSave_PressedAsync;
                    _ButtonCropSave.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }
                return _ButtonCropSave;
            }
        }
        private SfImageEditor _Editor;
        private SfImageEditor Editor {
            get {
                if (_Editor == null) {
                    _Editor = new SfImageEditor();
                    _Editor.BackgroundColor = Color.Transparent;
                    _Editor.ImageLoaded += Editor_ImageLoaded;
                    _Editor.EndReset += Editor_EndReset;
                    _Editor.ToolbarSettings.IsVisible = false;
                    _Editor.ImageSaving += Editor_ImageSaving;

                }
                return _Editor;
            }
        }

        private DimActivityIndicatorContentView _CustomActivityIndicator;
        private DimActivityIndicatorContentView CustomActivityIndicator {
            get {
                if (_CustomActivityIndicator == null) {
                    _CustomActivityIndicator = new DimActivityIndicatorContentView();
                }
                return _CustomActivityIndicator;
            }
        }
        #endregion

        #region Initialization
        public ImageCroppingContentPage() {
            this.Setup();

        }
        public ImageCroppingContentPage(List<string> encodedImages, User user, int slideshowNumber) {
            this.ViewModel.EncodedImages = encodedImages;
            this.ViewModel.SlideShowNumber = slideshowNumber;
            this.ViewModel.User = user;
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
            // TODO: Possibly look into disabling orientation on this page
        }
        #endregion

        #region Private API
        private void Setup() {

            this.Title = "Crop Image";
            Editor.Source = this.ViewModel.ImageFromBase64(this.ViewModel.EncodedImages[ViewModel.ImageIndex]).Source;


            Grid grid = new Grid {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
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
            grid.Children.Add(ButtonReset, 0, 1, 1, 2);
            grid.Children.Add(ButtonCropSave, 1, 2, 1, 2);

            RelativeLayout layout = new RelativeLayout();

            layout.Children.Add(grid, Constraint.Constant(0), Constraint.Constant(0),
            Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Height;
            }));

            layout.Children.Add(CustomActivityIndicator, Constraint.Constant(0),Constraint.Constant(0),
            Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Height;
            }));


            this.Content = layout;

        }

        private void LoadNextImage() {
            Editor.Source = this.ViewModel.ImageFromBase64(this.ViewModel.EncodedImages[ViewModel.ImageIndex]).Source;
        }


        #region Events
        private void Editor_ImageSaving(object sender, ImageSavingEventArgs args) {
            var stream = args.Stream;


            // convert to byte[]
            using (var memoryStream = new MemoryStream()) {
                stream.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();

                var imageHelper = DependencyService.Get<IImageHelper>();
                bytes = imageHelper.ResizeImage(bytes, 1200, 700);
                this.ViewModel.EncodedBytes.Add(bytes);
            }

            args.Cancel = true;
        }

        void ButtonReset_Clicked(object sender, EventArgs e) {
            Console.WriteLine("RESET");
            Editor.Reset();
        }

        async void ButtonCropSave_PressedAsync(object sender, EventArgs e) {
            if (this.ButtonCropSave.Text == "Crop") {
                Editor.Crop();
                this.ButtonCropSave.Text = "Save";
            } else if (this.ButtonCropSave.Text == "Save") {
                Editor.Save();
                if (this.ViewModel.CanLoadNextImage()) {
                    LoadNextImage();
                } else {
                    this.CustomActivityIndicator.IsRunning = true;

                    if (await this.ViewModel.SendImagesToServerAsync()) {
                        // successfully sent to server
                        this.CustomActivityIndicator.IsRunning = false;
                        await DisplayAlert("Success", "Your image(s) have been successfully uploaded.", "Ok");
                        await this.Navigation.PopAsync();

                    } else {
                        // error sending to server
                        this.CustomActivityIndicator.IsRunning = false;
                        await DisplayAlert("Error", "Oops, something went wrong. Please try again later.", "Ok");
                        await this.Navigation.PopAsync();

                    }
                }
            }
        }


        void Editor_EndReset(object sender, EndResetEventArgs args) {
            this.ButtonCropSave.Text = "Crop";
            Editor.ToggleCropping(12, 7);
        }
        private void Editor_ImageLoaded(object sender, ImageLoadedEventArgs args) {
            Editor.ToggleCropping(12, 7);
            this.ButtonCropSave.Text = "Crop";
        }

        #endregion

        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}