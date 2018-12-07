using System;
using Plugin.InAppBilling.Abstractions;

namespace EVSlideShow.Core.Models {
    public class BillingProduct : BaseModel {
        public InAppBillingProduct Product { get; set; }

    }
}
