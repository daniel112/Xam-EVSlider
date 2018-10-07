using System;
using EVSlideShow.Core.Components.Common;
using Xamarin.Forms;

namespace EVSlideShow.Core.Common {

    public sealed class AppSettings {
        #region Variables
        private static readonly Lazy<AppSettings> lazy = new Lazy<AppSettings>(() => new AppSettings());
        internal static bool IsDebugBuild {
            get {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        public string VersionNumber { 
            get {
                return DependencyService.Get<IAppSettingsVersionAndBuild>().GetVersionNumber();
            }
        }
        public string BuildNumber { 
            get {
                return DependencyService.Get<IAppSettingsVersionAndBuild>().GetBuildNumber();
            }
        }

        public static AppSettings Instance {
            get {
                return lazy.Value;
            }
        }
        #endregion
       
        #region PrivateAPI

        #endregion

        #region PublicAPI

        #endregion

    }



}
