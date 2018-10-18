using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.Common.DependencyInterface;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class ManageImageFileContentPage : BaseContentPage<ManageImageFileViewModel> {

        #region Variables
        private ScrollView _ScrollViewContent;
        private ScrollView ScrollViewContent {
            get {
                if (_ScrollViewContent == null) {
                    _ScrollViewContent = new ScrollView {
                        Margin = new Thickness(20),
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };
                }
                return _ScrollViewContent;
            }
        }

        private StackLayout _StackLayoutImageTitle;
        private StackLayout StackLayoutImageTitle {
            get {
                if (_StackLayoutImageTitle == null) {
                    _StackLayoutImageTitle = new StackLayout {
                        Orientation = StackOrientation.Horizontal
                    };
                }
                return _StackLayoutImageTitle;
            }
        }

        private FlexLayout _FlexLayoutMainContent;
        private FlexLayout FlexLayoutMainContent {
            get {
                if (_FlexLayoutMainContent == null) {
                    _FlexLayoutMainContent = new FlexLayout {
                        Direction = FlexDirection.Column,
                        JustifyContent = FlexJustify.Center,
                    };
                }
                return _FlexLayoutMainContent;
            }
        }

        private Image _ImageContentManage;
        private Image ImageContentManage {
            get {
                if (_ImageContentManage == null) {
                    _ImageContentManage = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "icon_manage",
                        WidthRequest = 60,
                        HeightRequest = 60,
                    };
                }
                return _ImageContentManage;
            }
        }

        private Label _LabelTitle;
        private Label LabelTitle {
            get {
                if (_LabelTitle == null) {
                    _LabelTitle = new Label {
                        Text = "Manage Image Files",
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 24,
                        TextColor = Color.FromHex(AppTheme.SecondaryTextColor()),
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(10, 0, 0, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelTitle.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelTitle;
            }
        }

        // TODO: need to make a custom view so we can put image next to the word
        private Button _ButtonUploadPhotos;
        private Button ButtonUploadPhotos {
            get {
                if (_ButtonUploadPhotos == null) {
                    _ButtonUploadPhotos = new Button {
                        Text = "Upload",
                        FontSize = 18,
                        TextColor = Color.FromHex("618ec6"),
                        BackgroundColor = Color.White,
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(30, 30, 30, 0)

                    };
                    _ButtonUploadPhotos.Clicked += ButtonUploadPhotos_Clicked;
                    _ButtonUploadPhotos.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }
                return _ButtonUploadPhotos;
            }
        }
        #endregion

        #region Initialization
        public ManageImageFileContentPage() {
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
        #endregion

        #region Private API
        private void Setup() {


            // stacklayout title
            this.StackLayoutImageTitle.Children.Add(this.ImageContentManage);
            this.StackLayoutImageTitle.Children.Add(this.LabelTitle);


            // flexlayout
            this.FlexLayoutMainContent.Children.Add(this.StackLayoutImageTitle);
            this.FlexLayoutMainContent.Children.Add(this.ButtonUploadPhotos);

            this.ScrollViewContent.Content = new StackLayout {
                Children = {
                    this.FlexLayoutMainContent,

                }
            };

            Content = this.ScrollViewContent;

        }

        // Buttons
        void ButtonUploadPhotos_Clicked(object sender, EventArgs e) {
            var mediaServie = DependencyService.Get<IMediaService>();

        }
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion


    }
}
