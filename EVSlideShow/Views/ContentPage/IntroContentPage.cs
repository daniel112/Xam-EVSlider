﻿using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using EVSlideShow.Core.Views.ContentViews;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class IntroContentPage : BaseContentPage<IntroViewModel> {

        #region Variables
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

        private Label _LabelTitleTop;
        private Label LabelTitleTop {
            get {
                if (_LabelTitleTop == null) {
                    _LabelTitleTop = new Label {
                        Text = "EV SLIDESHOW",
                        FontSize = 24,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        FontAttributes = FontAttributes.Bold,
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(30, 15, 30, 0)
                    };
                    _LabelTitleTop.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelTitleTop;
            }
        }

        private Label _LabelTitleBottom;
        private Label LabelTitleBottom {
            get {
                if (_LabelTitleBottom == null) {
                    _LabelTitleBottom = new Label {
                        Text = "For TESLA",
                        FontSize = 22,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(30, 0, 30, 0)
                    };
                    _LabelTitleBottom.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
                }

                return _LabelTitleBottom;
            }
        }

        private Label _LabelSummary;
        private Label LabelSummary {
            get {
                if (_LabelSummary == null) {
                    _LabelSummary = new Label {
                        Text = "TO VIEW THE FREE CONTENT SLIDESHOWS, CREATE AFREE ACCOUNT WITH NO OBLIGATIONS",
                        FontSize = 16,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(30, 20, 30, 0)
                    };
                    _LabelSummary.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }

                return _LabelSummary;
            }
        }

        private ContentView _ContentViewInstructionWrapper;
        private ContentView ContentViewInstructionWrapper {
            get {
                if (_ContentViewInstructionWrapper == null) {
                    _ContentViewInstructionWrapper = new ContentView {
                        BackgroundColor = Color.FromHex(AppTheme.SecondaryColor()),
                        Margin = new Thickness(30, 50, 30, 0),
                        Padding = new Thickness(12)
                    };
                }
                return _ContentViewInstructionWrapper;
            }
        }
        private Label _LabelInstruction;
        private Label LabelInstruction {
            get {
                if (_LabelInstruction == null) {
                    _LabelInstruction = new Label {
                        Text = "To create your personal photo slideshows with your own photos you will need to create an account and subscribe",
                        FontSize = 16,
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };
                    _LabelInstruction.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);

                }

                return _LabelInstruction;
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

        private Button _ButtonCreateAccount;
        private Button ButtonCreateAccount {
            get {
                if (_ButtonCreateAccount == null) {
                    _ButtonCreateAccount = new Button {
                        Text = "Create an Account",
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex("618ec6"),
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(30, 20, 30, 0)
                    };
                    _ButtonCreateAccount.Clicked += ButtonCreateAccount_Clicked;
                }
                return _ButtonCreateAccount;
            }
        }

        private Button _ButtonLogin;
        private Button ButtonLogin {
            get {
                if (_ButtonLogin == null) {
                    _ButtonLogin = new Button {
                        Text = "Login",
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.FromHex("618ec6"),
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(30, 20, 30, 0)

                    };
                    _ButtonLogin.Clicked += ButtonLogin_Clicked;
                }
                return _ButtonLogin;
            }
        }

        #endregion

        #region Lifecycle method
        public IntroContentPage() {
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }

        #endregion


        #region Private API


        private void Setup() {

            this.ContentViewInstructionWrapper.Content = this.LabelInstruction;

            ContentView imageview = new ContentView {
                Content = this.ImageLogo,

            };

            FlexLayout flexLayout = new FlexLayout {
                Direction = FlexDirection.Column,
                JustifyContent = FlexJustify.Center,
                Children = {
                    imageview,
                    this.LabelTitleTop,
                    this.LabelTitleBottom,
                    this.LabelSummary,
                    this.ContentViewInstructionWrapper,
                    this.ButtonCreateAccount,
                    this.ButtonLogin,
                },
            };

            this.ScrollViewContent.Content = flexLayout;


            Content = this.ScrollViewContent;
        }

        // UIResponder
        void ButtonCreateAccount_Clicked(object sender, EventArgs e) {
            this.Navigation.PushModalAsync(new SignUpContentPage());
        }

        void ButtonLogin_Clicked(object sender, EventArgs e) {
            this.Navigation.PushModalAsync(new LoginContentPage());
        }
        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
