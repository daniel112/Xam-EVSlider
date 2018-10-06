using System;
using System.Timers;
using EVSlideShow.Core.Common;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class SplashScreenContentPage : ContentPage {

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
                        FontFamily = "Microsoft PhagsPa",
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
                        FontFamily = "Microsoft PhagsPa",
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
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            this.StartTimer();
        }

        #endregion


        #region Private API
        private void Setup() {
            this.BackgroundColor = Color.FromHex(AppTheme.PrimaryColor());

            // TODO: check up on application resources onPlatform stuff
            // TODO: put font in appTheme with onPlatform stuff
            object check;
            Application.Current.Resources.TryGetValue("AppFont", out check);
            OnPlatform<string> stuff = (OnPlatform<string>)check;
            ////////////////

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
                Application.Current.MainPage = new LoginContentPage();
            });

        }
        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
