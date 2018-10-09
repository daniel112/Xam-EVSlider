using System;
using EVSlideShow.Core.ViewModels.Base;

namespace EVSlideShow.Core.ViewModels {
    public class SignUpViewModel : BaseViewModel {

        #region Variables
        internal string Username;
        internal string Email;
        internal string EmailRepeat;
        internal string Password;
        internal string EVType;
        internal bool DidViewTermsOfUse;

        #endregion
        public SignUpViewModel() {
        }
    }
}
