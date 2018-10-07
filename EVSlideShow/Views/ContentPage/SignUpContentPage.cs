using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.ViewModels.Base;
using EVSlideShow.Core.Views.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class SignUpContentPage : BaseContentPage<BaseViewModel> {
        public SignUpContentPage() {
            this.BackgroundColor = Color.Red;
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }
    }
}
