using System;
using System.Collections.Generic;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.ViewModels.Base;

namespace EVSlideShow.Core.ViewModels {
    public class SignUpViewModel : BaseViewModel {

        #region Variables
        internal string Username = "";
        internal string Email = "";
        internal string EmailRepeat = "";
        internal string Password = "";
        internal string EVType = "";
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

        #region Public API
        public bool AllInputFilledOut() {
            if (Username.Trim() != "" && Email.Trim() != "" && EmailRepeat.Trim() == Email.Trim() && Password.Trim() != "" && EVType.Trim() != "" && DidAcceptTermsOfUse) {
                Console.WriteLine($"Username:{Username} \n Email:{Email} \n EVType:{EVType} \n Password: {Password}");
                return true;
            }
            return false;
        }

        public User GenerateUserFromInput() {
            var user = new User();
            user.Username = this.Username.ToLower().Trim();
            user.Email = this.Email.ToLower().Trim();
            user.Password = this.Password;
            user.EVType = this.EVType;

            return user;
        }

        public string ValidateInput() {
            string message = "";
            if (this.Password.Length < 5) {
                message += "Password is too short (minimum is 5 characters)";
            }
            return message;
        }
        #endregion
    }
}
