using System;
using Xamarin.Forms;

namespace EVSlideShow.Core.Views {
    
    public class RootMasterDetailPage : MasterDetailPage {

        #region Variables

        #endregion

        #region Initialization
        public RootMasterDetailPage(Page masterPage, Page detailPage) {
            MasterBehavior = MasterBehavior.Popover;
            // Master = side menu content
            Master = masterPage;
            Detail = detailPage;
        }

         
        #endregion


        #region Private API


        #endregion


        #region Public API

        #endregion

        #region Delegates

        #endregion
    }
}
