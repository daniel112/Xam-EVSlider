using System;
using Plugin.InAppBilling.Abstractions;

namespace EVSlideShow.Core.Models {
    public class BillingItem : BaseModel {
        public InAppBillingProduct Product { get; set; }
        public InAppBillingPurchase PurchasedItem { get; set; }

    }
}
