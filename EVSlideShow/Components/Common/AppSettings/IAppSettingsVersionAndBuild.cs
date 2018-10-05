using System;
namespace EVSlideShow.Core.Components.Common {
    public interface IAppSettingsVersionAndBuild {  
        string GetVersionNumber();  
        string GetBuildNumber();  
    } 
}
