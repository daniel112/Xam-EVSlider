using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace EVSlideShow.Core.Components.Helpers {
    public static class PermissionHelper {

        public static async Task<PermissionStatus> GetPermissionStatusForPhotoLibraryAsync() {
            try {
                PermissionStatus status = PermissionStatus.Granted;
                // need access to photos
                var photosStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

                if (photosStatus != PermissionStatus.Granted) {
                    // request permission
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);

                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];

                }

                return status;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return PermissionStatus.Unknown;
            }
        }
    }
}
