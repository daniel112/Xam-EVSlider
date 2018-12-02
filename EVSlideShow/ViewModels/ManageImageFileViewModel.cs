﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EVSlideShow.Core.Models;
using EVSlideShow.Core.Network.Managers;
using EVSlideShow.Core.ViewModels.Base;
using Xamarin.Forms;

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

        public async Task<NetworkDebug> DeleteAll() {
            var manager = new ImageNetworkManager();
            return await manager.DeleteAll(SlideShowNumber, User.AuthToken);

        }

        public async Task<NetworkDebug> DeleteByID(string ids) {
            var manager = new ImageNetworkManager();
            return await manager.DeletePhotosByID(ids, SlideShowNumber, User.AuthToken);

        }

    }
}
