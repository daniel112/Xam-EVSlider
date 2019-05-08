using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;

namespace EVSlideShow.Droid.Common.Helpers {

    public static class PhotoUtilHelper {

        #region Variables

        #endregion

        #region Initialization

        #endregion

        #region Private API
        private static Bitmap Rotate(Bitmap bitmap, float degrees) {
            Matrix matrix = new Matrix();
            matrix.PostRotate(degrees);
            return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        }

        private static Bitmap Flip(Bitmap bitmap, bool horizontal, bool vertical) {
            Matrix matrix = new Matrix();
            matrix.PreScale(horizontal ? -1 : 1, vertical ? -1 : 1);
            return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        }




        private static String GetDataColumn(Context context, Android.Net.Uri uri, String selection, String[] selectionArgs) {
            ICursor cursor = null;
            String column = "_data";
            String[] projection = { column };

            try {
                cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs, null);
                if (cursor != null && cursor.MoveToFirst()) {
                    int index = cursor.GetColumnIndexOrThrow(column);
                    return cursor.GetString(index);
                }
            } finally {
                if (cursor != null)
                    cursor.Close();
            }
            return null;
        }

        //Whether the Uri authority is ExternalStorageProvider.
        private static bool IsExternalStorageDocument(Android.Net.Uri uri) {
            return "com.android.externalstorage.documents".Equals(uri.Authority);
        }

        //Whether the Uri authority is DownloadsProvider.
        private static bool IsDownloadsDocument(Android.Net.Uri uri) {
            return "com.android.providers.downloads.documents".Equals(uri.Authority);
        }

        //Whether the Uri authority is MediaProvider.
        private static bool IsMediaDocument(Android.Net.Uri uri) {
            return "com.android.providers.media.documents".Equals(uri.Authority);
        }

        //Whether the Uri authority is Google Photos.
        private static bool IsGooglePhotosUri(Android.Net.Uri uri) {
            return "com.google.android.apps.photos.content".Equals(uri.Authority);
        }

        private static Android.Media.Orientation GetOrientation(Context context, Android.Net.Uri photoUri) {
            var cursor = context.ContentResolver.Query(photoUri, new String[] { MediaStore.Images.ImageColumns.Orientation }, null, null, null);

            if (cursor.Count != 1) {
                cursor.Close();
                return Android.Media.Orientation.Undefined;
                //return -1;
            }

            cursor.MoveToFirst();
            int orientation = cursor.GetInt(0);
            cursor.Close();
            cursor = null;

            switch (orientation) {
                case 90:
                    return Android.Media.Orientation.Rotate90;
                case 180:
                    return Android.Media.Orientation.Rotate180;
                case 270:
                    return Android.Media.Orientation.Rotate270;
                default:
                    return Android.Media.Orientation.Normal;
            }

        }

        #endregion

        #region Public API
        public static byte[] UpdateAndConvertURI(Context context, Android.Net.Uri photoUri) {
            System.IO.Stream stream = context.ContentResolver.OpenInputStream(photoUri);

            // slow
            Bitmap bitmap = BitmapFactory.DecodeStream(stream);
            bitmap = UpdateOrientation(context, photoUri, GetActualPathFromURI(photoUri, context), bitmap);

            MemoryStream memStream = new MemoryStream();

            // slow
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memStream);

            return memStream.ToArray();

        }

        public static Bitmap UpdateOrientation(Context context, Android.Net.Uri photoUri, string absolutePath, Bitmap bitmap) {

            var orientation = (int)GetOrientation(context, photoUri);
            switch (orientation) {
                case (int)Android.Media.Orientation.Rotate90:
                    return Rotate(bitmap, 90);

                case (int)Android.Media.Orientation.Rotate180:
                    return Rotate(bitmap, 180);

                case (int)Android.Media.Orientation.Rotate270:
                    return Rotate(bitmap, 270);

                case (int)Android.Media.Orientation.FlipHorizontal:
                    return Flip(bitmap, true, false);

                case (int)Android.Media.Orientation.FlipVertical:
                    return Flip(bitmap, false, true);

                default:
                    return bitmap;
            }
        }


        public static string GetActualPathFromURI(Android.Net.Uri uri, Context context) {
            bool isKitKat = Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat;

            if (isKitKat && DocumentsContract.IsDocumentUri(context, uri)) {
                // ExternalStorageProvider
                if (IsExternalStorageDocument(uri)) {
                    string docId = DocumentsContract.GetDocumentId(uri);

                    char[] chars = { ':' };
                    string[] split = docId.Split(chars);
                    string type = split[0];

                    if ("primary".Equals(type, StringComparison.OrdinalIgnoreCase)) {
                        return Android.OS.Environment.ExternalStorageDirectory + "/" + split[1];
                    }
                }
                // DownloadsProvider
                else if (IsDownloadsDocument(uri)) {
                    string id = DocumentsContract.GetDocumentId(uri);

                    Android.Net.Uri contentUri = ContentUris.WithAppendedId(
                                    Android.Net.Uri.Parse("content://downloads/public_downloads"), long.Parse(id));

                    return GetDataColumn(context, contentUri, null, null);
                }
                // MediaProvider
                else if (IsMediaDocument(uri)) {
                    String docId = DocumentsContract.GetDocumentId(uri);

                    char[] chars = { ':' };
                    String[] split = docId.Split(chars);

                    String type = split[0];

                    Android.Net.Uri contentUri = null;
                    if ("image".Equals(type)) {
                        contentUri = MediaStore.Images.Media.ExternalContentUri;
                    } else if ("video".Equals(type)) {
                        contentUri = MediaStore.Video.Media.ExternalContentUri;
                    } else if ("audio".Equals(type)) {
                        contentUri = MediaStore.Audio.Media.ExternalContentUri;
                    }

                    String selection = "_id=?";
                    String[] selectionArgs = new String[] {
                        split[1]
                    };

                    return GetDataColumn(context, contentUri, selection, selectionArgs);
                }
            }
            // MediaStore (and general)
            else if ("content".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase)) {

                // Return the remote address
                return IsGooglePhotosUri(uri) ? uri.LastPathSegment : GetDataColumn(context, uri, null, null);
            }
            // File
            else if ("file".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase)) {
                return uri.Path;
            }

            return null;
        }
        #endregion

        #region Delegates

        #endregion
    }
}
