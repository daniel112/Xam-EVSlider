using System;
namespace EVSlideShow.Core.Components.Helpers {
    public static class ObjectSerializerHelper {

        public static string ConvertObjectToBase64(object obj) {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
        }

        public static T Convertbase64StringToObject<T>(string base64String) {
            byte[] byteArray = Convert.FromBase64String(base64String);
            string json = System.Text.Encoding.UTF8.GetString(byteArray);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }


    }
}
