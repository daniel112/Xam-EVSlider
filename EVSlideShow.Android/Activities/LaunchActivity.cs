
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EVSlideShow.Droid.Activities {
    [Activity(MainLauncher = true, NoHistory = true,
             Label = "EVSlideShow", Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round")]
    public class LaunchActivity : AppCompatActivity {
        static readonly string TAG = "X:" + typeof(LaunchActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState) {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume() {
            base.OnResume();
            Task startupWork = new Task(() => { StartMainActivity(); });
            startupWork.Start();
        }

        async void StartMainActivity() {
            await Task.Delay(500);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
