using System;
using System.Timers;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Constants;
using EVSlideShow.Views;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class SplashContentPage : ContentPage {

        #region Variables
        private Timer timer;
        private Image _ImageLogo;
        private Image ImageLogo {
            get {
                if (_ImageLogo == null) {
                    _ImageLogo = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "splash_icon",
                        WidthRequest = 80,
                        HeightRequest = 80,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
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
                        FontSize = 24,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        FontAttributes = FontAttributes.Bold,
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(30, 15, 30, 0)
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
                        FontSize = 22,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
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
        public SplashContentPage() {
            this.Setup();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            this.StartTimer();
        }

        #endregion


        #region Private API
        private void Setup() {
            this.BackgroundColor = Color.FromHex(AppTheme.PrimaryColor());

            // set custom style
            this.LabelTop.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);
            this.LabelBottom.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
            Console.WriteLine(this.LabelTop.FontSize);

            AbsoluteLayout layout = new AbsoluteLayout();

            this.StackContent.Children.Add(this.ImageLogo);
            this.StackContent.Children.Add(this.LabelTop);
            this.StackContent.Children.Add(this.LabelBottom);

            // center stack
            AbsoluteLayout.SetLayoutFlags(this.StackContent, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.StackContent, new Rectangle(0.5, 0.5, 300, AbsoluteLayout.AutoSize));
            layout.Children.Add(this.StackContent);

            Content = layout;
        }

        private void StartTimer() {
            // 2 seconds
            timer = new Timer(1500);
            timer.Elapsed += new ElapsedEventHandler(Timer_ElapsedHandler);
            timer.Start();
        }

        private void Timer_ElapsedHandler(object source, ElapsedEventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                timer.Stop();
                Application.Current.MainPage = new IntroContentPage();
            });

        }
        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
