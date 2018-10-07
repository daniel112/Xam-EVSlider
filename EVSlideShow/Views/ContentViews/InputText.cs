﻿using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.Core.Constants;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views.ContentViews {

    public interface IInputTitle {
        void Input_TextChanged(string text, InputText inputText);
        void Input_DidPressReturn(string text, InputText inputText);

    }
    public class InputText : ContentView {
        #region Variables

        public IInputTitle Delegate;
        public string Identifier;
        public string InputTitle {
            set {
                this.LabelTitle.Text = value;
            }
        }
        public string InputPlaceholder {
            set {
                this.EntryItem.Placeholder = value;
            }
        }
        public bool IsPassword {
            set {
                this.EntryItem.IsPassword = value;
            }
        }
        public string ImageName {
            set {
                this.ImageLogo.Source = value;
            }
        }
        private Image _ImageLogo;
        private Image ImageLogo {
            get {
                if (_ImageLogo == null) {
                    _ImageLogo = new Image {
                        Aspect = Aspect.AspectFit,
                        WidthRequest = 24,
                        HeightRequest = 24,
                        Margin = new Thickness(15, 0,0,0)
                    };
                }
                return _ImageLogo;
            }
        }
        private BorderlessEntry _EntryItem;
        private BorderlessEntry EntryItem {
            get {
                if (_EntryItem == null) {
                    _EntryItem = new BorderlessEntry {
                        TextColor = Color.White,
                        PlaceholderColor = Color.White.MultiplyAlpha(0.3),
                        HeightRequest = 50,
                        BackgroundColor = Color.Transparent,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(0, 0, 10, 0)
                    };
                    _EntryItem.TextChanged += EntryItem_TextChanged;
                    _EntryItem.Completed += EntryItem_Completed;

                }
                return _EntryItem;
            }
        }

        private Frame _FrameWrapper;
        private Frame FrameWrapper {
            get {
                if (_FrameWrapper == null) {
                    _FrameWrapper = new Frame {
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.White.MultiplyAlpha(0.5),
                        HasShadow = false,
                        CornerRadius = 8,
                        Padding = new Thickness(0)
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
                        TextColor = Color.FromHex(AppTheme.DefaultTextColor()),
                        HorizontalTextAlignment = TextAlignment.Start,
                        BackgroundColor = Color.Transparent
                    };
                    _LabelTitle.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily);
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
                        Orientation = StackOrientation.Horizontal
                    };

                }
                return _StackLayoutHorizontal;
            }
        }

        private StackLayout _StackLayoutVertical;
        private StackLayout StackLayoutVertical {
            get {
                if (_StackLayoutVertical == null) {
                    _StackLayoutVertical = new StackLayout {
                        BackgroundColor = Color.Transparent,
                        Orientation = StackOrientation.Vertical
                    };

                }
                return _StackLayoutVertical;
            }
        }
        #endregion


        public InputText() {
        }

        public InputText(string identifier, string title, string placeholder, bool isPasswordType, string imageLogo, IInputTitle inputDelegate) {
            this.Identifier = identifier;
            this.InputTitle = title;
            this.InputPlaceholder = placeholder;
            this.IsPassword = isPasswordType;
            this.ImageName = imageLogo;
            this.Delegate = inputDelegate;
            this.Setup();
        }

        #region Private API
        private void Setup() {

            // wrapper
            this.FrameWrapper.Content = this.StackLayoutHorizontal;

            // horizontal stack
            this.StackLayoutHorizontal.Children.Add(this.ImageLogo);
            this.StackLayoutHorizontal.Children.Add(this.EntryItem);

            // vertical stack
            this.StackLayoutVertical.Children.Add(this.LabelTitle);
            this.StackLayoutVertical.Children.Add(this.FrameWrapper);

            this.Content = this.StackLayoutVertical;

        }

        #region EventHandlers

        void EntryItem_TextChanged(object sender, EventArgs e) {
            BorderlessEntry entry = (BorderlessEntry)sender;
            this.Delegate.Input_TextChanged(entry.Text, this);
        }
        void EntryItem_Completed(object sender, EventArgs e) {
            BorderlessEntry entry = (BorderlessEntry)sender;
            this.Delegate.Input_DidPressReturn(entry.Text, this);

        }


        #endregion
        #endregion
    }
}
