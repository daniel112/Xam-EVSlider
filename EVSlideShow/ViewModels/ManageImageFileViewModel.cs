using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EVSlideShow.Core.Components.Helpers;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.Network.Managers;
using EVSlideShow.Core.ViewModels.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.ViewModels {
    public class ManageImageFileViewModel : BaseViewModel {
        public readonly string kAppleSubscriptionDisclaimer = "Your iTunes account will be charged at confirmation of purchase. Your subscription can be managed in iTunes Account Settings and will auto-renew " +
        	"unless disabled 24 hours prior to the end of billing cycle. Free trials ends at time of purchase.";
        public readonly string kAndroidSubscriptionDisclaimer = "Your account will be charged at confirmation of purchase. Your subscription can be managed in your Account Settings and will auto-renew " +
    "unless disabled 24 hours prior to the end of billing cycle. Free trials ends at time of purchase.";
        public bool ShouldDisplayWarning = true;
        public User _User;
        public User User {
            get {
                if (_User == null) {
                    _User = new User();
                }
                return _User;
            }
            set {
                _User = value;
            }
        }
        public int SlideShowNumber = 1;
        public string InitialTitle {
            get {
                if (User.IsSubscribed) {
                    return "Managing Slideshow #1";
                }
                return "Home";
            }
        }
        public string[] ToolbarOptions {
            get {
                if (User.HasMultipleSubscription) {
                    return new string[] { "Slideshow #1", "Slideshow #2", "Slideshow #3", "Logout" };
                }
                if (User.IsSubscribed) {
                    return new string[] { "Slideshow #1", "Logout" };
                }
                return new string[] { "Logout" };
            }
        }

        public ManageImageFileViewModel() {
        }

        public async Task<NetworkDebug> DeleteAll() {
            var manager = new ImageNetworkManager();
            return await manager.DeleteAll(SlideShowNumber, User.AuthToken);

        }

        public async Task<NetworkDebug> DeleteByID(string ids) {
            var manager = new ImageNetworkManager();
            return await manager.DeletePhotosByID(ids, SlideShowNumber, User.AuthToken);

        }

        public async Task<bool> UserSingleSubscribe() {
            var manager = new UserNetworkManager();

            if (await manager.UserSubscribeAsync(User.AuthToken)) {
                User.IsSubscribed = true;
                // save user login data to app data
                Application.Current.Properties["User"] = ObjectSerializerHelper.ConvertObjectToBase64(User);
                await Application.Current.SavePropertiesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> UserMultipleSubscribe() {
            var manager = new UserNetworkManager();

            if (await manager.UserMultipleSubscribeAsync(User.AuthToken)) {
                User.HasMultipleSubscription = true;
                // save user login data to app data
                Application.Current.Properties["User"] = ObjectSerializerHelper.ConvertObjectToBase64(User);
                await Application.Current.SavePropertiesAsync();
                return true;
            }
            return false;

        }
    }
}
