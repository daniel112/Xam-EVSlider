using System;
using EVSlideShow.Core.Common;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class SplashScreenContentPage : ContentPage {

        #region Variables
        private Image _ImageLogo;
        private Image ImageLogo {
            get {
                if (_ImageLogo == null) {
                    _ImageLogo = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "bow",
                        WidthRequest = 330,
                    };
                }
                return _ImageLogo;
            }
            set {
                _ImageLogo = value;
            }
        }

        private Label _LabelTop;
        private Label LabelTop {
            get {
                if (_LabelTop == null) {
                    _LabelTop = new Label {
                        Text = "EV SLIDESHOW",
                        FontSize = 17,
                        FontFamily = "Cambria", // iOS only TODO: add android ttf
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(30, 0, 30, 0)
                    };
                }

                return _LabelTop;
            }
        }

        private Label _LabelBottom;
        private Label LabelBottom {
            get {
                if (_LabelBottom == null) {
                    _LabelBottom = new Label {
                        Text = "For TESLA",
                        FontSize = 17,
                        FontFamily = "Cambria", // iOS only TODO: add android ttf
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(30, 0, 30, 0)
                    };
                }

                return _LabelBottom;
            }
        }

        private StackLayout _StackContent;
        private StackLayout StackContent {
            get {
                if (_StackContent == null) {
                    _StackContent = new StackLayout();
                }
                return _StackContent;
            }
        }
        #endregion

        #region Lifecycle method
        public SplashScreenContentPage() {
            this.Setup();

            AbsoluteLayout layout = new AbsoluteLayout();

            this.StackContent.Children.Add(this.ImageLogo);
            this.StackContent.Children.Add(this.LabelTop);
            this.StackContent.Children.Add(this.LabelBottom);

            // center stack
            AbsoluteLayout.SetLayoutFlags(this.StackContent, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.StackContent, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            layout.Children.Add(this.StackContent);

        }



        #endregion


        #region Private API
        private void Setup() {
            this.BackgroundColor = Color.FromHex(AppTheme.PrimaryColor());
        }


        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
