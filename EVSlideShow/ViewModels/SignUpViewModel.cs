using System;
using System.Collections.Generic;
using EVSlideShow.Core.ViewModels.Base;

namespace EVSlideShow.Core.ViewModels {
    public class SignUpViewModel : BaseViewModel {

        #region Variables
        internal string Username = "";
        internal string Email = "";
        internal string EmailRepeat = "";
        internal string Password = "";
        internal string EVType ="";
        internal bool DidViewTermsOfUse = false;
        internal bool DidAcceptTermsOfUse = false;

        private List<string> _PickerItems;
        internal List<string> PickerItems {
            get {
                if (_PickerItems == null) {
                    _PickerItems = new List<string> {
                        "Tesla Model S",
                        "Tesla Model X",
                        "Tesla Model 3"
                    };
                    this.EVType = _PickerItems[0];
                }
                return _PickerItems;
            }
        }
        #endregion
        public SignUpViewModel() {
        }
    }
}
