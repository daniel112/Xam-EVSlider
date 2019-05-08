using System;
using EVSlideShow.Core.Common;
using Xamarin.Forms;

namespace EVSlideShow.Views {
    public class BaseNavigationPage : NavigationPage {
        public BaseNavigationPage() {
        }

        public BaseNavigationPage(Page root) : base(root) {
            BarBackgroundColor = Color.FromHex(AppTheme.PrimaryColor());
            BarTextColor = Color.FromHex(AppTheme.DefaultTextColor());
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height) {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}
