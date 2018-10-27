using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace EVSlideShow.Core.Components.Helpers {
    public static class PermissionHelper {

        public static async Task<PermissionStatus> GetPermissionStatusForPhotoLibraryAsync() {
            try {
                PermissionStatus status = PermissionStatus.Granted;
                // need access to media library, photos, and storage
                var mediaLibraryStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.MediaLibrary);
                var photosStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                var externalStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                
                if (mediaLibraryStatus != PermissionStatus.Granted || photosStatus != PermissionStatus.Granted || externalStatus != PermissionStatus.Granted) {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.MediaLibrary)) {
                        Console.WriteLine("NEED PERMISSION ANDROID");
                    }

                    // request permission
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.MediaLibrary, Permission.Photos, Permission.Storage);

                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.MediaLibrary))
                        status = results[Permission.MediaLibrary];

                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];

                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                }

                return status;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return PermissionStatus.Unknown;
            }
        }
    }
}
