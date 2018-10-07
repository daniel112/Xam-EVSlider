using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using Plugin.InputKit.Shared.Controls;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class LoginContentPage : BaseContentPage<LoginViewModel> {

        #region Variables
        private string TextUsername;
        private string TextPassword;
        private FlexLayout _FlexLayoutContent;
        private FlexLayout FlexLayoutContent {
            get {
                if (_FlexLayoutContent == null) {
                    _FlexLayoutContent = new FlexLayout {
                        Direction = FlexDirection.Column,
                        JustifyContent = FlexJustify.Center,
                    };
                }
                return _FlexLayoutContent;
            }
        }
        private ScrollView _ScrollViewContent;
        private ScrollView ScrollViewContent {
            get {
                if (_ScrollViewContent == null) {
                    _ScrollViewContent = new ScrollView();
                }
                return _ScrollViewContent;
            }
        }

        private ContentView _ContentViewImage;
        private ContentView ContentViewImage {
            get {
                if (_ContentViewImage == null) {
                    _ContentViewImage = new ContentView {
                        Margin = new Thickness(0, 0, 0, 30),
                    };
                }
                return _ContentViewImage;
            }
        }

        private Image _ImageLogo;
        private Image ImageLogo {
            get {
                if (_ImageLogo == null) {
                    _ImageLogo = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "splash_icon",
                        WidthRequest = 80,
                        HeightRequest = 80,
                    };
                }
                return _ImageLogo;
            }
            set {
                _ImageLogo = value;
            }
        }

        private Button _ButtonLogin;
        private Button ButtonLogin {
            get {
                if (_ButtonLogin == null) {
                    _ButtonLogin = new Button {
                        Text = "Log In",
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex("618ec6"),
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(30, 30, 30, 0)

                    };
                    _ButtonLogin.Clicked += ButtonLogin_Clicked;
                    _ButtonLogin.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }
                return _ButtonLogin;
            }
        }

        private Button _ButtonClose;
        private Button ButtonClose {
            get {
                if (_ButtonClose == null) {
                    _ButtonClose = new Button {
                        Image = "nav_close",
                        Margin = new Thickness(20, 20, 0, 0),
                        WidthRequest = 36,
                        HeightRequest = 36,
                        HorizontalOptions = LayoutOptions.Start,

                    };
                    _ButtonClose.Clicked += ButtonClose_Clicked;

                }
                return _ButtonClose;
            }
        }

        private Label _LabelForgotPassword;
        private Label LabelForgotPassword {
            get {
                if (_LabelForgotPassword == null) {
                    _LabelForgotPassword = new Label {
                        Text = "Forgot Password?",
                        HorizontalTextAlignment = TextAlignment.End,
                        FontSize = 16,
                        TextColor = Color.FromHex(AppTheme.SecondaryTextColor()),
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(30, 10, 30, 0),
                        //WidthRequest = 100,
                        HorizontalOptions = LayoutOptions.End,
                    };
                    _LabelForgotPassword.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += LabelForgotPassword_Tapped;
                    _LabelForgotPassword.GestureRecognizers.Add(tapGestureRecognizer);
                }

                return _LabelForgotPassword;
            }
        }

        private Label _LabelUsername;
        private Label LabelUsername {
            get {
                if (_LabelUsername == null) {
                    _LabelUsername = new Label {
                        Text = "Username",
                        FontSize = 18,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        HorizontalTextAlignment = TextAlignment.Start,
                        Margin = new Thickness(30, 0, 30, 10)
                    };
                    _LabelUsername.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                }

                return _LabelUsername;
            }
        }

        private Label _LabelPassword;
        private Label LabelPassword {
            get {
                if (_LabelPassword == null) {
                    _LabelPassword = new Label {
                        Text = "Password",
                        FontSize = 18,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Start,
                        Margin = new Thickness(30, 20, 30, 10)
                    };
                    _LabelPassword.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                }

                return _LabelPassword;
            }
        }


        private AdvancedEntry _EntryUsername;
        private AdvancedEntry EntryUsername {
            get {
                if (_EntryUsername == null) {
                    _EntryUsername = new AdvancedEntry {
                        Placeholder = "Name",
                        TextColor = Color.White,
                        PlaceholderColor = Color.White.MultiplyAlpha(0.3),
                        BorderColor = Color.White.MultiplyAlpha(0.5),
                        Margin = new Thickness(30, 0, 30, 0),
                        HeightRequest = 50,
                        BackgroundColor = Color.Transparent,
                        CornerRadius = 8
                    };
                    _EntryUsername.Completed += EntryUsername_Completed;
                    _EntryUsername.TextChanged += EntryUsername_TextChanged;
                }
                return _EntryUsername;
            }
        }

        private AdvancedEntry _EntryPassword;
        private AdvancedEntry EntryPassword {
            get {
                if (_EntryPassword == null) {
                    _EntryPassword = new AdvancedEntry {
                        Placeholder = "Password",
                        TextColor = Color.White,
                        PlaceholderColor = Color.White.MultiplyAlpha(0.3),
                        BorderColor = Color.White.MultiplyAlpha(0.5),
                        Margin = new Thickness(30, 0, 30, 0),
                        HeightRequest = 50,
                        BackgroundColor = Color.Transparent,
                        CornerRadius = 8,
                        Annotation = AdvancedEntry.AnnotationType.Password
                    };
                    _EntryPassword.Completed += EntryPassword_Completed;
                    _EntryPassword.TextChanged += EntryPassword_TextChanged;
                }
                return _EntryPassword;
            }

        }
        #endregion

        #region Initialization
        public LoginContentPage() {
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
        #endregion

        #region Private API
        private void Setup() {

            // image
            this.ContentViewImage.Content = this.ImageLogo;

            FlexLayout flexLayoutMainContent = new FlexLayout {
                Direction = FlexDirection.Column,
                JustifyContent = FlexJustify.Center,
                Children = {
                    this.ContentViewImage,
                    this.LabelUsername,
                    this.EntryUsername,
                    this.LabelPassword,
                    this.EntryPassword,
                    this.ButtonLogin,
                },
            };

            this.ScrollViewContent.Content = new StackLayout {
                Children = {
                    flexLayoutMainContent,
                    this.LabelForgotPassword
                }
            };

            StackLayout stacklayout = new StackLayout {
                Children = {
                    this.ButtonClose,
                    this.ScrollViewContent
                }
            };
            Content = stacklayout;
        }

        private void ValidateLogin() {
            Console.WriteLine($"Username: {this.TextUsername} Password: {this.TextPassword}");
        }

        #region EventHandlers
        // Buttons
        void ButtonLogin_Clicked(object sender, EventArgs e) {
            this.ValidateLogin();
        }

        void ButtonClose_Clicked(object sender, EventArgs e) {
            this.Navigation.PopModalAsync(true);
        }

        // GestureRecognizers
        void LabelForgotPassword_Tapped(object sender, EventArgs e) {
            Console.WriteLine("TAPPED");
        }

        // AdvancedEntry
        void EntryPassword_TextChanged(object sender, EventArgs e) {
            AdvancedEntry entry = (AdvancedEntry)sender;
            this.TextPassword = entry.Text;
        }
        void EntryPassword_Completed(object sender, EventArgs e) {
            AdvancedEntry entry = (AdvancedEntry)sender;
            this.ValidateLogin();

        }

        void EntryUsername_TextChanged(object sender, EventArgs e) {
            AdvancedEntry entry = (AdvancedEntry)sender;
            this.TextUsername = entry.Text;
        }
        void EntryUsername_Completed(object sender, EventArgs e) {
            AdvancedEntry entry = (AdvancedEntry)sender;
            entry.FocusNext();
        }

        #endregion
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
