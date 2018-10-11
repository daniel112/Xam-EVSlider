using System;
using System.Collections.Generic;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.CustomRenderers;
using EVSlideShow.Core.Constants;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views.ContentViews {

    public interface IInputPickerDelegate {
        void InputPicker_SelectedIndexChanged(object selectedItem, InputPickerContentView input);
        void InputPicker_DidPressReturn(string text, InputPickerContentView input);

    }
    public class InputPickerContentView : ContentView {

        #region Variables
        public IInputPickerDelegate Delegate;
        public string Identifier;

        public double TitleFontSize {
            set {
                this.LabelTitle.FontSize = value;
            }
        }
        public string InputTitle {
            set {
                this.LabelTitle.Text = value;
            }
        }
        public List<string>PickerItems {
            get {
                return (List<string>)this.PickerEVType.Items;
            }
            set {
                this.PickerEVType.ItemsSource = value;
            }
        }


        private Image _ImageIndicator;
        private Image ImageIndicator {
            get {
                if (_ImageIndicator == null) {
                    _ImageIndicator = new Image {
                        Source = "nav_down_arrow",
                        Aspect = Aspect.AspectFit,
                        WidthRequest = 36,
                        HeightRequest = 36,
                        Margin = new Thickness(0, 0, 10, 0),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    };
                }
                return _ImageIndicator;
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

        private PickerBorderless _PickerEVType;
        private PickerBorderless PickerEVType {
            get {
                if (_PickerEVType == null) {
                    _PickerEVType = new PickerBorderless {
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        FontSize = 16,
                        HeightRequest = 50,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(10, 0, 0, 0),

                    };
                    _PickerEVType.SelectedIndexChanged += PickerEVType_SelectedIndexChanged;
                }
                return _PickerEVType;
            }
        }
        #endregion

        #region Initialization
        public InputPickerContentView() {
        }

        public InputPickerContentView(string identifier, string title, List<string>items, IInputPickerDelegate inputDelegate) {
            this.Identifier = identifier;
            this.InputTitle = title;
            this.Delegate = inputDelegate;

            this.PickerItems = items;
            this.PickerEVType.SelectedItem = items[0];

            this.Setup();
        }
        #endregion

        #region Private API

        private void Setup() {

            // wrapper
            this.FrameWrapper.Content = this.StackLayoutHorizontal;

            // horizontal stack
            this.StackLayoutHorizontal.Children.Add(this.PickerEVType);
            this.StackLayoutHorizontal.Children.Add(this.ImageIndicator);

            // vertical stack
            this.StackLayoutVertical.Children.Add(this.LabelTitle);
            this.StackLayoutVertical.Children.Add(this.FrameWrapper);

            this.Content = this.StackLayoutVertical;

            // gestures
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }


        // UIResponder
        void PickerEVType_SelectedIndexChanged(object sender, EventArgs e) {
            Picker picker = (Picker)sender;
            this.Delegate.InputPicker_SelectedIndexChanged(picker.SelectedItem, this);
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e) {
            this.PickerEVType.Focus();
        }
        #endregion

        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
