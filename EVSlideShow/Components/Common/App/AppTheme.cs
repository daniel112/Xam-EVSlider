using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Plugin.Share.Abstractions;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Plugin.Share.Abstractions;

namespace EVSlideShow.Core.Common {
    public sealed class AppTheme {
        #region Variables
        private static readonly Lazy<AppTheme> lazy = new Lazy<AppTheme>(() => new AppTheme());

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
        public static ShareColor ShareColorMain() {
            return new ShareColor(42,52,68, 255);
        }
        public static string DefaultTextColor() {
            return "#ffffff";
        }
        public static string SecondaryTextColor() {
            return "#618ec3";
        }
        public static string SecondaryColor() {
            return "#405978";
        }
        public static string PrimaryColor() {
            return "#2a3444";
        }

        #endregion

    }
}
