using System;
using System.Threading.Tasks;
using EVSlideShow.Core.Common;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {

    public interface IInputButtonPopupPage {
        void DidTapButton(string text);
    }

    public class InputButtonPopupPage : PopupPage {

        #region Variables
        public IInputButtonPopupPage PageDelegate;
        private StackLayout _FlexLayoutWrapper;
        private StackLayout FlexLayoutWrapper {
            get {
                if (_FlexLayoutWrapper == null) {
                    _FlexLayoutWrapper = new StackLayout {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        BackgroundColor = Color.FromHex(AppTheme.SecondaryColor()),
                        WidthRequest = 280
                    };
                }
                return _FlexLayoutWrapper;
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
                        LineBreakMode = LineBreakMode.WordWrap,
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        HeightRequest = 50,
                        BackgroundColor = Color.FromHex(AppTheme.PrimaryColor()),
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor())
                    };
                }
                return _LabelTitle;
            }
        }

        private Entry _Entry;
        private Entry Entry {
            get {
                if (_Entry == null) {
                    _Entry = new Entry {
                        BackgroundColor = Color.Transparent,
                        TextColor = Color.White,
                        HeightRequest = 40
                    };
                    _Entry.Completed += Entry_Completed;

                }
                return _Entry;
            }
        }

        private readonly StackLayout ButtonStackLayout = new StackLayout {
            Orientation = StackOrientation.Horizontal,
            Spacing = 0,
            HeightRequest = 40
        };

        private Button _ButtonAction;
        private Button ButtonAction {
            get {
                if (_ButtonAction == null) {
                    _ButtonAction = new Button {
                        Text = "Action Button",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex(AppTheme.PrimaryColor()),
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        CornerRadius = 0,
                        WidthRequest = 140
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
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex(AppTheme.SecondaryColor()),
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        CornerRadius = 0,
                    };
                    _ButtonCancel.Clicked += ButtonCancel_Clicked;
                }
                return _ButtonCancel;
            }
        }

        #endregion

        #region Initialization
        public InputButtonPopupPage() {
            SetupContent();
        }

        public InputButtonPopupPage(string title, string buttonText) {
            LabelTitle.Text = title;
            ButtonAction.Text = buttonText;
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

            // button stack
            ButtonStackLayout.Children.Add(ButtonAction);
            ButtonStackLayout.Children.Add(ButtonCancel);

            FlexLayoutWrapper.Children.Add(LabelTitle);
            FlexLayoutWrapper.Children.Add(Entry);
            FlexLayoutWrapper.Children.Add(ButtonStackLayout);

            this.Content = FlexLayoutWrapper;

        }

        private async void ClosePopupAsync() {
            await Navigation.RemovePopupPageAsync(this);
        }
        // UIResponder
        private void ButtonAction_Clicked(object sender, EventArgs e) {
            ClosePopupAsync();
            PageDelegate.DidTapButton(Entry.Text);
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e) {
            ClosePopupAsync();
        }
        #endregion


        #region Public API

        #endregion

        #region Delegates
        void Entry_Completed(object sender, EventArgs e) {
            var text = ((Entry)sender).Text;
            ClosePopupAsync();
            PageDelegate.DidTapButton(text);

        }
        #endregion
    }
}
