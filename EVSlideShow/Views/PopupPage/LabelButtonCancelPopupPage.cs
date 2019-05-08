using System;
using System.Threading.Tasks;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Constants;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {

    public interface ILabelButtonCancelPopupPage {
        void DidTapButton(LabelButtonCancelPopupPage page, object output);
        void DidTapDisclaimer(LabelButtonCancelPopupPage page);

    }

    public class LabelButtonCancelPopupPage : PopupPage {

        #region Variables
        public ILabelButtonCancelPopupPage PageDelegate;
        private StackLayout _StackLayoutWrapper;
        private StackLayout StackLayoutWrapper {
            get {
                if (_StackLayoutWrapper == null) {
                    _StackLayoutWrapper = new StackLayout {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        BackgroundColor = Color.FromHex(AppTheme.SecondaryColor()),
                        WidthRequest = 280
                    };
                }
                return _StackLayoutWrapper;
            }
        }

        private Label _LabelTitle;
        private Label LabelTitle {
            get {
                if (_LabelTitle == null) {
                    _LabelTitle = new Label() {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        Text = "Title Here",
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold,
                        HeightRequest = 40,
                        BackgroundColor = Color.FromHex(AppTheme.PrimaryColor()),
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor())
                    };
                }
                return _LabelTitle;
            }
        }

        private ScrollView _ScrollViewMessage;
        private ScrollView ScrollViewMessage {
            get {
                if (_ScrollViewMessage == null) {
                    _ScrollViewMessage = new ScrollView {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.Transparent
                    };
                }
                return _ScrollViewMessage;
            }
        }

        private Label _LabelMessage;
        private Label LabelMessage {
            get {
                if (_LabelMessage == null) {
                    _LabelMessage = new Label() {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        LineBreakMode = LineBreakMode.WordWrap,
                        Text = "Message placeholder",
                        FontSize = 15,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(10)
                    };
                }
                return _LabelMessage;
            }
        }

        private Label _LabelDisclaimer;
        private Label LabelDisclaimer {
            get {
                if (_LabelDisclaimer == null) {
                    _LabelDisclaimer = new Label {
                        Text = "",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 11,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = new Thickness(5, 0, 5, 0)
                    };
                    _LabelDisclaimer.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                }

                return _LabelDisclaimer;
            }
        }
        private Label _LabelDisclaimerInteractable;
        private Label LabelDisclaimerInteractable {
            get {
                if (_LabelDisclaimerInteractable == null) {
                    _LabelDisclaimerInteractable = new Label {
                        Text = "",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 11,
                        TextColor = Color.DarkBlue,
                        BackgroundColor = Color.Transparent,
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = new Thickness(5, 0, 5, 10)
                    };
                    _LabelDisclaimerInteractable.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += LabelDisclaimerInteractable_Tapped;
                    _LabelDisclaimerInteractable.GestureRecognizers.Add(tapGestureRecognizer);
                }

                return _LabelDisclaimerInteractable;
            }
        }

        private Button _ButtonAction;
        private Button ButtonAction {
            get {
                if (_ButtonAction == null) {
                    _ButtonAction = new Button {
                        Text = "Action Button",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex(AppTheme.PrimaryColor()),
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        CornerRadius = 0,
                        HeightRequest = 40
                    };
                    _ButtonAction.Clicked += ButtonAction_Clicked;
                }
                return _ButtonAction;
            }
        }

        private Button _ButtonCancel;
        private Button ButtonCancel {
            get {
                if (_ButtonCancel == null) {
                    _ButtonCancel = new Button {
                        Text = "Cancel",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.White,
                        TextColor = Color.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        CornerRadius = 0,
                        HeightRequest = 40
                    };
                    _ButtonCancel.Clicked += ButtonCancel_Clicked;
                }
                return _ButtonCancel;
            }
        }

        #endregion

        #region Initialization
        public LabelButtonCancelPopupPage() {
            SetupContent();
        }

        public LabelButtonCancelPopupPage(string title, string message, string buttonText, int? messageHeightLimit) {
            LabelTitle.Text = title;
            LabelMessage.Text = message;
            ButtonAction.Text = buttonText;
            ScrollViewMessage.HeightRequest = messageHeightLimit != null ? (double)messageHeightLimit : 200;
            SetupContent();
        }

        public LabelButtonCancelPopupPage(string title, string message, string buttonText, string disclaimerText, string disclaimerInteractableText, int? messageHeightLimit) {
            LabelTitle.Text = title;
            LabelMessage.Text = message;
            ButtonAction.Text = buttonText;
            LabelDisclaimer.Text = disclaimerText;
            LabelDisclaimerInteractable.Text = disclaimerInteractableText;
            ScrollViewMessage.HeightRequest = messageHeightLimit != null ? (double)messageHeightLimit : 200;
            SetupContent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        }

        protected override void OnAppearingAnimationBegin() {
            base.OnAppearingAnimationBegin();
        }

        protected override Task OnAppearingAnimationEndAsync() {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync() {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override bool OnBackgroundClicked() {
            // return false if you don't want popup to close when background is clicked
            Console.WriteLine("background tapped");
            return false;
        }
        #endregion


        #region Private API
        private void SetupContent() {
            // scrollview
            ScrollViewMessage.Content = LabelMessage;

            StackLayoutWrapper.Children.Add(LabelTitle);
            StackLayoutWrapper.Children.Add(ScrollViewMessage);
            if (LabelDisclaimer.Text != "") {
                StackLayoutWrapper.Children.Add(LabelDisclaimer);
            }

            if (LabelDisclaimerInteractable.Text != "") {
               StackLayoutWrapper.Children.Add(LabelDisclaimerInteractable);
            }



            StackLayoutWrapper.Children.Add(ButtonAction);
            StackLayoutWrapper.Children.Add(ButtonCancel);

            this.Content = StackLayoutWrapper;

        }

        private async void ClosePopupAsync() {
            await Navigation.RemovePopupPageAsync(this);
        }
        // UIResponder
        private void ButtonAction_Clicked(object sender, EventArgs e) {
            ClosePopupAsync();
            PageDelegate.DidTapButton(this, LabelTitle.Text);
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e) {
            ClosePopupAsync();
        }

        // GestureRecognizers
        void LabelDisclaimerInteractable_Tapped(object sender, EventArgs e) {
            PageDelegate.DidTapDisclaimer(this);
        }
        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
