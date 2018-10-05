using System;
using EVSlideShow.Core.Common;
using Xamarin.Forms;

namespace EVSlideShow.Views {
    public class BaseNavigationPage : NavigationPage {
        public BaseNavigationPage() {
        }

        public BaseNavigationPage(Page root) : base(root) {
            BarBackgroundColor = Color.FromHex(AppTheme.Instance.MainColor);
            BarTextColor = Color.FromHex(AppTheme.DefaultTextColor());
        }
    }
}
