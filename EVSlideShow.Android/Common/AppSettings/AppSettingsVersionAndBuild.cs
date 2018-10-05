using System;
using Android.Content;
using Android.Content.PM;
using EVSlideShow.Core.Components.Common;
using EVSlideShow.Droid.Common.AppSettings;

[assembly: Xamarin.Forms.Dependency(typeof(AppSettingsVersionAndBuild))]
namespace EVSlideShow.Droid.Common.AppSettings {
    public class AppSettingsVersionAndBuild : IAppSettingsVersionAndBuild {

        private readonly Context _Context = Android.App.Application.Context;
        public string GetBuildNumber() {
            return _Context.PackageManager.GetPackageInfo(_Context.PackageName, PackageInfoFlags.MetaData).VersionCode.ToString();
        }

        public string GetVersionNumber() {            
            return _Context.PackageManager.GetPackageInfo(_Context.PackageName, PackageInfoFlags.MetaData).VersionName;  
        }
    }
}
