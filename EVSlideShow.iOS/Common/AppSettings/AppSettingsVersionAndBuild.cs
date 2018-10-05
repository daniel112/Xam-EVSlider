using System;
using System.Runtime.CompilerServices;
using EVSlideShow.Core.Components.Common;
using EVSlideShow.iOS.Common.AppSettings;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(AppSettingsVersionAndBuild))]
namespace EVSlideShow.iOS.Common.AppSettings {
    public class AppSettingsVersionAndBuild : IAppSettingsVersionAndBuild {

        public string GetBuildNumber() {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();  
        }

        public string GetVersionNumber() {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
    }
}
