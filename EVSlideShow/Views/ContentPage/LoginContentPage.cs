using System;
using EVSlideShow.Core.Common;
using EVSlideShow.Core.ViewModels;
using EVSlideShow.Core.Views.Base;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    public class LoginContentPage : BaseContentPage<LoginViewModel> {
    
        #region Variables

        #endregion

        #region Lifecycle method
        public LoginContentPage() {
            this.Setup();
        }

        protected override void OnOrientationUpdate(DeviceOrientatione orientation) {
        }

        #endregion


        #region Private API
        private void Setup() {
            this.BackgroundColor = Color.FromHex(AppTheme.PrimaryColor());
        }


        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
