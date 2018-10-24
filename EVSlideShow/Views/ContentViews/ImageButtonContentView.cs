using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Constants;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views.ContentViews {

    public interface IImageButtonDelegate {
        void ImageButton_DidPress(string buttonText,ImageButtonContentView button);

    }

    public class ImageButtonContentView : ContentView {

        #region Variables
        public IImageButtonDelegate Delegate;

        public string LabelText {
            set {
                this.LabelTitle.Text = value;
            }
        }

        private Image _ImageLeft;
        private Image ImageLeft {
            get {
                if (_ImageLeft == null) {
                    _ImageLeft = new Image {
                        Source = "icon_photo",
                        Aspect = Aspect.AspectFit,
                        WidthRequest = 36,
                        HeightRequest = 36,
                        Margin = new Thickness(10, 0, 10, 0),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                    };
                }
                return _ImageLeft;
            }
        }

        private Frame _FrameWrapper;
        private Frame FrameWrapper {
            get {
                if (_FrameWrapper == null) {
                    _FrameWrapper = new Frame {
                        BackgroundColor = Color.White,
                        BorderColor = Color.White.MultiplyAlpha(0.5),
                        HasShadow = false,
                        CornerRadius = 8,
                        Padding = new Thickness(0), // required since Frame has a default padding
                        HeightRequest = 50
                    };

                }
                return _FrameWrapper;
            }
        }

        private Label _LabelTitle;
        private Label LabelTitle {
            get {
                if (_LabelTitle == null) {
                    _LabelTitle = new Label {
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("618ec6"),
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        BackgroundColor = Color.Transparent
                    };
                    _LabelTitle.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);
                }

                return _LabelTitle;
            }
        }

        private StackLayout _StackLayoutHorizontal;
        private StackLayout StackLayoutHorizontal {
            get {
                if (_StackLayoutHorizontal == null) {
                    _StackLayoutHorizontal = new StackLayout {
                        BackgroundColor = Color.Transparent,
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };

                }
                return _StackLayoutHorizontal;
            }
        }
        #endregion

        #region Initialization
        public ImageButtonContentView() {
        }

        public ImageButtonContentView(string text, IImageButtonDelegate @delegate) {

            this.LabelText = text;
            this.Delegate = @delegate;
            this.Setup();

        }
        #endregion

        #region Private API
        private void Setup() {

            // wrapper
            this.FrameWrapper.Content = this.StackLayoutHorizontal;

            // horizontal stack
            this.StackLayoutHorizontal.Children.Add(this.ImageLeft);
            this.StackLayoutHorizontal.Children.Add(this.LabelTitle);


            this.Content = this.FrameWrapper;

            // gestures
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e) {
            this.Delegate.ImageButton_DidPress(this.LabelTitle.Text, this);
        }
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}

