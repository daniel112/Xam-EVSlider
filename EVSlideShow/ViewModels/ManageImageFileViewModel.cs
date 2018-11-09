using System;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.ViewModels.Base;

namespace EVSlideShow.Core.ViewModels {
    public class ManageImageFileViewModel : BaseViewModel {

        public User _User;
        public User User {
            get {
                if (_User == null) {
                    _User = new User();
                }
                return _User;
            }
            set {
                _User = value;
            }
        }
        public int SlideShowNumber = 1;

        public ManageImageFileViewModel() {
        }
    }
}
