using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Plugin.Share.Abstractions;

namespace EVSlideShow.Core.Common {
    public sealed class AppTheme {
        #region Variables
        private static readonly Lazy<AppTheme> lazy = new Lazy<AppTheme>(() => new AppTheme());

        public string MainColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TextColor { get; set; }
        public string SideMenuHeaderIcon { get; set; }
        public string IconTintColor { get; set; } 
        public static AppTheme Instance {
            get {
                return lazy.Value;
            }
        }

        #endregion

        #region Initialization

        #endregion


        #region Private API
      

        #endregion


        #region Public API

        public static string DefaultTextColor() {
            return "#ffffff";
        }

        public static string DefaultBakcgroundColor() {
            return "#CCCCCC";
        }
        #endregion

    }
}
