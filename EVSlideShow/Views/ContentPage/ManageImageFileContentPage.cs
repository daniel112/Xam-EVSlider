using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.Components.Common.DependencyInterface;
using EVSlideShow.Core.Components.Helpers;
using EVSlideShow.Core.Components.Managers;
using EVSlideShow.Core.Constants;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using EVSlideShow.Core.Views.ContentViews;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Linq;

namespace EVSlideShow.Core.Views {
    public class ManageImageFileContentPage : BaseContentPage<ManageImageFileViewModel>, IImageButtonDelegate, IInputButtonPopupPage {

        #region Variables
        private ScrollView _ScrollViewContent;
        private ScrollView ScrollViewContent {
            get {
                if (_ScrollViewContent == null) {
                    _ScrollViewContent = new ScrollView {
                        Margin = new Thickness(20),
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };
                }
                return _ScrollViewContent;
            }
        }

        private StackLayout _StackLayoutImageTitle;
        private StackLayout StackLayoutImageTitle {
            get {
                if (_StackLayoutImageTitle == null) {
                    _StackLayoutImageTitle = new StackLayout {
                        Orientation = StackOrientation.Horizontal
                    };
                }
                return _StackLayoutImageTitle;
            }
        }

        private FlexLayout _FlexLayoutMainContent;
        private FlexLayout FlexLayoutMainContent {
            get {
                if (_FlexLayoutMainContent == null) {
                    _FlexLayoutMainContent = new FlexLayout {
                        Direction = FlexDirection.Column,
                        JustifyContent = FlexJustify.Center,
                    };
                }
                return _FlexLayoutMainContent;
            }
        }

        private Image _ImageContentManage;
        private Image ImageContentManage {
            get {
                if (_ImageContentManage == null) {
                    _ImageContentManage = new Image {
                        Aspect = Aspect.AspectFit,
                        Source = "icon_manage",
                        WidthRequest = 60,
                        HeightRequest = 60,
                        HorizontalOptions = LayoutOptions.Center,
                    };
                }
                return _ImageContentManage;
            }
        }

        private ContentView _ContentViewImage;
        private ContentView ContentViewImage {
            get {
                if (_ContentViewImage == null) {
                    _ContentViewImage = new ContentView {
                    };
                }
                return _ContentViewImage;
            }
        }

        private Label _LabelInstruction;
        private Label LabelInstruction {
            get {
                if (_LabelInstruction == null) {
                    _LabelInstruction = new Label {
                        Text = "Type and save the below URL in your Tesla screen to access slideshows and free content",
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(20, 20, 20, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelInstruction.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelInstruction;
            }
        }

        private Label _LabelURL;
        private Label LabelURL {
            get {
                if (_LabelURL == null) {
                    _LabelURL = new Label {
                        Text = "https://evslideshow.com/login",
                        HorizontalTextAlignment = TextAlignment.Center,
                        LineBreakMode = LineBreakMode.WordWrap,
                        FontSize = 20,
                        TextColor = Color.White,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(10, 20, 10, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelURL.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelURL;
            }
        }

        private Label _LabelMessage;
        private Label LabelMessage {
            get {
                if (_LabelMessage == null) {
                    _LabelMessage = new Label {
                        Text = "Select up to 30 photos to display within EV Slideshow. Upload 5 photos at a time and wait for confirmation",
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        TextColor = Color.White,
                        BackgroundColor = Color.Transparent,
                        Margin = new Thickness(20, 20, 20, 0),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    _LabelMessage.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }

                return _LabelMessage;
            }
        }

        private ImageButtonContentView _ImageButtonUploadPhotos;
        private ImageButtonContentView ImageButtonUploadPhotos {
            get {
                if (_ImageButtonUploadPhotos == null) {
                    _ImageButtonUploadPhotos = new ImageButtonContentView("Upload", this);
                }
                return _ImageButtonUploadPhotos;
            }
        }

        private ImageButtonContentView _ImageButtonDeletePhotos;
        private ImageButtonContentView ImageButtonDeletePhotos {
            get {
                if (_ImageButtonDeletePhotos == null) {
                    _ImageButtonDeletePhotos = new ImageButtonContentView("Delete", this);
                }
                return _ImageButtonDeletePhotos;
            }
        }

        private Button _ButtonSubscribe;
        private Button ButtonSubscribe {
            get {
                if (_ButtonSubscribe == null) {
                    _ButtonSubscribe = new Button {
                        Text = "SUBSCRIBE",
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        BackgroundColor = Color.White,
                        CornerRadius = 8,
                        HeightRequest = 50,
                        Margin = new Thickness(0, 30, 0, 0),

                    };
                    _ButtonSubscribe.Clicked += ButtonSubscribe_ClickedAsync;
                    _ButtonSubscribe.SetDynamicResource(StyleProperty, ApplicationResourcesConstants.StyleLabelFontFamily_Bold);

                }
                return _ButtonSubscribe;
            }
        }

        private Grid _GridButtons;
        private Grid GridButtons {
            get {
                if (_GridButtons == null) {
                    _GridButtons = new Grid {
                        Margin = new Thickness(10, 20, 10, 0),
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        RowDefinitions = {
                            new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) }, // row 0                        
                            new RowDefinition { Height = GridLength.Star }, // row 1
                         },
                        ColumnDefinitions = {
                            new ColumnDefinition { Width =  new GridLength(1, GridUnitType.Star) }, // col 0
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // col 1
                        },
                    };
                }
                return _GridButtons;
            }
        }

        private ToolbarItem _ToolbarUser;
        private ToolbarItem ToolbarUser {
            get {
                if (_ToolbarUser == null) {
                    _ToolbarUser = new ToolbarItem {
                        Icon = "icon_user"
                    };
                    _ToolbarUser.Clicked += ToolbarUser_ClickedAsync;
                }

                return _ToolbarUser;
            }
        }

        private DimActivityIndicatorContentView _CustomActivityIndicator;
        private DimActivityIndicatorContentView CustomActivityIndicator {
            get {
                if (_CustomActivityIndicator == null) {
                    _CustomActivityIndicator = new DimActivityIndicatorContentView();
                }
                return _CustomActivityIndicator;
            }
        }
        #endregion

        #region Initialization
        public ManageImageFileContentPage() {
        }

        public ManageImageFileContentPage(User user) {
            this.ViewModel.User = user;
            MessagingCenterSubscribe();
            this.Setup();

        }
        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

        #endregion

        #region Private API
        private void Setup() {

            Title = ViewModel.InitialTitle;
            // image
            this.ContentViewImage.Content = this.ImageContentManage;


            // Toolbar items
            this.ToolbarItems.Add(ToolbarUser);


            // GridButtons
            // grid.Children.Add(item ,col, col+colSpan, row, row+rowspan)
            this.GridButtons.Children.Add(ImageButtonUploadPhotos, 0, 1, 0, 1);
            this.GridButtons.Children.Add(ImageButtonDeletePhotos, 1, 2, 0, 1);
            this.GridButtons.Children.Add(ButtonSubscribe, 0, 2, 1, 2);


            // Label wrapping is buggy, so we put the wrapped label in 1x1 grid
            var gridSummary = new Grid {
                RowDefinitions = {
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }, // row 0                        
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }, // row 1
                         }
            };
            gridSummary.Children.Add(this.LabelURL, 0, 0);
            gridSummary.Children.Add(this.LabelMessage, 0, 1);

            // flexlayout
            this.FlexLayoutMainContent.Children.Add(this.ContentViewImage);
            this.FlexLayoutMainContent.Children.Add(this.LabelInstruction);
            this.FlexLayoutMainContent.Children.Add(gridSummary);
            this.FlexLayoutMainContent.Children.Add(this.GridButtons);

            this.ScrollViewContent.Content = new StackLayout {
                Children = {
                    this.FlexLayoutMainContent,

                }
            };

            RelativeLayout relativelayout = new RelativeLayout();

            // stack
            relativelayout.Children.Add(ScrollViewContent, Constraint.Constant(0), Constraint.Constant(0),
            Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Height;
            }));

            // loading
            relativelayout.Children.Add(CustomActivityIndicator, Constraint.Constant(0), Constraint.Constant(0),
            Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }), Constraint.RelativeToParent((parent) => {
                return parent.Height;
            }));

            Content = relativelayout;

        }

        private async Task<bool> IsUserSubscribedAsync() {
            if (!this.ViewModel.User.IsSubscribed) {
                await DisplayAlert("No Subscription Found", "Your account is not currently subscribed. Only subscribers have access to photo uploads. You can subscribe by hitting the 'Subscribe' button", "Ok");
                return false;
            }
            return true;
        }

        #region MessagingCenter

        private void MessagingCenterSubscribe() {
            MessagingCenter.Subscribe<object, object>(this, MessagingKeys.DidFinishSelectingImages, MessagingCenter_SendToCropViewAsync);
            MessagingCenter.Subscribe<object>(this, MessagingKeys.ShowLoadingIndicator, MessagingCenter_ShowLoadingIndicator);

        }

        private void MessagingCenterUnsubscribe() {
            MessagingCenter.Unsubscribe<List<string>>(this, MessagingKeys.DidFinishSelectingImages);
            MessagingCenter.Unsubscribe<List<string>>(this, MessagingKeys.ShowLoadingIndicator);

        }

        void MessagingCenter_ShowLoadingIndicator(object sender) {
            if (!CustomActivityIndicator.IsRunning) { this.CustomActivityIndicator.IsRunning = true; }
            CustomActivityIndicator.Message = "Compressing image(s)...";
        }

        async void MessagingCenter_SendToCropViewAsync(object sender, object obj) {

            List<byte[]> bytesList = ((System.Collections.IList)obj).Cast<byte[]>().ToList();
            if (bytesList.Count == 0) return; // no images went through, bug

            await this.Navigation.PushAsync(new ImageCroppingContentPage(bytesList, this.ViewModel.User, this.ViewModel.SlideShowNumber));
            if (CustomActivityIndicator.IsRunning) { this.CustomActivityIndicator.IsRunning = false; }

        }
        #endregion

        #region Buttons

        async void ButtonSubscribe_ClickedAsync(object sender, EventArgs e) {
            if (!await IsUserSubscribedAsync()) { return; }

            return;
            if (!ViewModel.User.IsSubscribed) {
                // needs subscribe to single first
                var product = await BillingManager.GetIAPBillingProductWithTypeAsync(EVeSubscriptionType.SingleSubscription);
                if (product.Success) {
                    // TODO: Display information about the product + confirmation view
                    Console.WriteLine(product.Product.Name);
                    Console.WriteLine(product.Product.Description);
                    Console.WriteLine(product.Product.LocalizedPrice);
                } else {
                    if (!String.IsNullOrEmpty(product.Message)) {
                        await DisplayAlert("Error", product.Message, "Ok");
                    } else {
                        await DisplayAlert("Error", "Oops, something went wrong. Please try again later.", "Ok");
                    }
                    return;
                }
            } else if (!ViewModel.User.HasMultipleSubscription) {
                // they already have single so they can opt for multiple
                var product = await BillingManager.GetIAPBillingProductWithTypeAsync(EVeSubscriptionType.AdditionalSubscription);
                if (product.Success) {
                    // TODO: Display information about the product + confirmation view
                    Console.WriteLine(product.Product.Name);
                    Console.WriteLine(product.Product.Description);
                    Console.WriteLine(product.Product.LocalizedPrice);
                } else {
                    if (!String.IsNullOrEmpty(product.Message)) {
                        await DisplayAlert("Error", product.Message, "Ok");
                    } else {
                        await DisplayAlert("Error", "Oops, something went wrong. Please try again later.", "Ok");
                    }
                    return;
                }
            }

        }

        async void ToolbarUser_ClickedAsync(object sender, EventArgs e) {

            var option = await DisplayActionSheet("Select Slideshow", "Cancel", null, ViewModel.ToolbarOptions);
            switch (option) {
                case "Slideshow #1":
                    this.ViewModel.SlideShowNumber = 1;
                    Title = "Managing Slideshow #1";
                    break;
                case "Slideshow #2":
                    this.ViewModel.SlideShowNumber = 2;
                    Title = "Managing Slideshow #2";
                    break;
                case "Slideshow #3":
                    this.ViewModel.SlideShowNumber = 3;
                    Title = "Managing Slideshow #3";
                    break;
                case "Logout":
                    Application.Current.Properties.Clear();
                    await Application.Current.SavePropertiesAsync();
                    Application.Current.MainPage = new IntroContentPage();
                    break;
            }

        }
        #endregion

        #endregion

        #region Public API

        #endregion

        #region Delegates
        async void IImageButtonDelegate.ImageButton_DidPress(string buttonText, ImageButtonContentView button) {

            CustomActivityIndicator.Message = "";
            if (!await IsUserSubscribedAsync()) { return; }

            if (buttonText == "Upload") {

                var status = await PermissionHelper.GetPermissionStatusForPhotoLibraryAsync();
                if (status == PermissionStatus.Granted) {
                    var mediaServie = DependencyService.Get<IMediaService>();
                    mediaServie.OpenGallery();
                } else {
                    await DisplayAlert("Permission Status", "Please enable access to photo library by going to your app settings.", "Ok");
                }

            } else {
                var action = await DisplayActionSheet("Delete Photos", "Cancel", null, "Delete by #", "Delete All");
                switch (action) {
                    case "Delete All":
                        this.CustomActivityIndicator.IsRunning = true;
                        var networkResult = await this.ViewModel.DeleteAll();
                        if (networkResult.Success) {
                            // successful
                            await DisplayAlert("Success", $"All photos for Slideshow #{ViewModel.SlideShowNumber} have been deleted. Please refresh car browser to see changes", "Ok");

                        } else {
                            // unsuccessful
                            await DisplayAlert("Error", $"Something went wrong, please try again later.", "Ok");
                        }
                        // DEBUGGING
                        //await DisplayAlert($"{(int)networkResult.StatusCode} : {networkResult.StatusCode.ToString()}", $"JSON message: {networkResult.Message}", "Ok");
                        this.CustomActivityIndicator.IsRunning = false;
                        break;

                    case "Delete by #":
                        var popupPage = new InputButtonPopupPage("Enter slide number(s) separated by commas", "Delete") {
                            PageDelegate = this
                        };
                        await Navigation.PushPopupAsync(popupPage);
                        break;
                }


            }
        }


        async void IInputButtonPopupPage.DidTapButton(string text) {
            if (string.IsNullOrEmpty(text)) {
                return;
            }
            this.CustomActivityIndicator.IsRunning = true;
            var networkResult = await this.ViewModel.DeleteByID(text);
            if (networkResult.Success) {
                //successful
                await DisplayAlert("Success", $"Image ID(s):{text} have been deleted. Please refresh car browser to see changes", "Ok");
            } else {
                // unsuccessful
                await DisplayAlert("Error", $"Something went wrong, please try again later.", "Ok");
            }
            //await DisplayAlert($"{(int)networkResult.StatusCode} : {networkResult.StatusCode.ToString()}", $"JSON message: {networkResult.Message}", "Ok");
            this.CustomActivityIndicator.IsRunning = false;

        }

        #endregion


    }
}
