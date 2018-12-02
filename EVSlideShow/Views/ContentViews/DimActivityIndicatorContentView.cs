using System;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views.ContentViews {
    public class DimActivityIndicatorContentView : ContentView {

        #region Variables
        public bool IsRunning {
            get {
                return ActivityIndicator.IsRunning;
            }
            set {
                ActivityIndicator.IsRunning = value;
                this.IsVisible = value;
            }
        }

        public string Message {
            get {
                return LabelMessage.Text;
            }
            set {
                LabelMessage.Text = value;
            }
        }

        private ActivityIndicator _ActivityIndicator;
        private ActivityIndicator ActivityIndicator {
            get {
                if (_ActivityIndicator == null) {
                    _ActivityIndicator = new ActivityIndicator {
                        IsRunning = false,
                        IsVisible = true,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Color = Color.White,
                        Scale = 1.5

                    };
                }
                return _ActivityIndicator;
            }
        }

        private Label _LabelMessage;
        private Label LabelMessage {
            get {
                if (_LabelMessage == null) {
                    _LabelMessage = new Label {
                        TextColor = Color.White,
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand, 
                        VerticalOptions = LayoutOptions.Center,
                    };
                }
                return _LabelMessage;
            }
        }
        #endregion

        #region Initialization
        public DimActivityIndicatorContentView() {
            this.Setup();
        }
        #endregion

        #region Private API
        private void Setup() {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            this.IsVisible = false;
            this.BackgroundColor = Color.Black.MultiplyAlpha(0.5);

            this.Content = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand, Children = { ActivityIndicator, LabelMessage } };
        }
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
