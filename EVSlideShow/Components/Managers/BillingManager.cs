using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Xamarin.Forms;
using System.Linq;
using EVSlideShow.Core.Models;

namespace EVSlideShow.Core.Components.Managers {
    public enum EVeSubscriptionType {
        Unknown,
        SingleSubscription,
        AdditionalSubscription
    }

    public static class BillingManager {
        #region Variables
        private static readonly IInAppBilling Billing = CrossInAppBilling.Current;
        private static string SingleSubscriptionProductID {
            get {
                switch (Device.RuntimePlatform) {
                    case Device.iOS:
                        return "EV_Slide_Show_Subscription";
                    case Device.Android:
                        return "evslideshow.subscription.single_slideshow";
                    default:
                        return "";
                }
            }
        }
        private static string AdditionalSubscriptionProductID {
            get {
                switch (Device.RuntimePlatform) {
                    case Device.iOS:
                        return "EV_Two_Extra_Slide_Shows_Subscriptions";
                    case Device.Android:
                        return "evslideshow.subscription.additional_slideshows";
                    default:
                        return "";
                }
            }
        }
        #endregion

        #region Private API

        #endregion

        #region Public API
        public static async Task<BillingItem> PurchaseProductWithTypeAsync(EVeSubscriptionType type) {

            var item = new BillingItem();

            try {
#if DEBUG
                Billing.InTestingMode = true;

#endif
                var connected = await Billing.ConnectAsync();
                if (!connected) {
                    item.Message = "Error connecting to store. Check your connection and try again.";
                }

                string subscriptionProductID;
                switch (type) {
                    case EVeSubscriptionType.Unknown:
                        subscriptionProductID = "";
                        break;
                    case EVeSubscriptionType.SingleSubscription:
                        subscriptionProductID = SingleSubscriptionProductID;
                        break;
                    case EVeSubscriptionType.AdditionalSubscription:
                        subscriptionProductID = AdditionalSubscriptionProductID;
                        break;
                    default:
                        subscriptionProductID = "";
                        break;
                }

                //try to purchase item
                var purchase = await Billing.PurchaseAsync(subscriptionProductID, ItemType.Subscription, "apppayload");
                if (purchase == null) {
                    item.Success = false;
                } else {
                    item.PurchasedItem = purchase;
                    item.Success = true;
                }
            } catch (InAppBillingPurchaseException purchaseEx) {
                item.Message = $"Error in {purchaseEx.Message}";
                item.Success = false;
            } catch (Exception ex) {
                item.Message = $"Issue connecting: {ex.Message}";
                item.Success = false;
            } finally {
                //Disconnect, it is okay if we never connected
                await Billing.DisconnectAsync();
            }

            return item;
        }



        public static async Task<BillingItem> GetIAPBillingProductWithTypeAsync(EVeSubscriptionType type) {

            var product = new BillingItem();
            try {
                var connected = await Billing.ConnectAsync();

                if (!connected) {
                    product.Message = "Error connecting to store. Check your connection and try again.";
                    product.Success = false;
                }
                IEnumerable<InAppBillingProduct> items = null;
                switch (type) {
                    case EVeSubscriptionType.Unknown:
                        items = null;
                        break;
                    case EVeSubscriptionType.SingleSubscription:
                        items = await Billing.GetProductInfoAsync(ItemType.Subscription, SingleSubscriptionProductID);
                        break;
                    case EVeSubscriptionType.AdditionalSubscription:
                        items = await Billing.GetProductInfoAsync(ItemType.Subscription, AdditionalSubscriptionProductID);
                        break;
                }
                if (items != null) {
                    product.Product = items.FirstOrDefault();
                    product.Success = true;
                } else {
                    product.Success = false;
                    product.Message = "Unable to retrieve product information";
                }

            } catch (InAppBillingPurchaseException purchaseEx) {
                //Billing Exception handle this based on the type
                product.Message = $"Error in {purchaseEx.Message}";
                product.Success = false;
            } catch (Exception ex) {
                product.Message = $"Issue connecting: {ex.Message}";
                product.Success = false;
            } finally {
                //Disconnect, it is okay if we never connected
                await Billing.DisconnectAsync();
            }

            return product;
        }
        #endregion

        #region Delegates

        #endregion
    }
}
