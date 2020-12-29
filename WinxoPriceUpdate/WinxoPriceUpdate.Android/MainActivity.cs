using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Globalization;
using System.Threading;

namespace WinxoPriceUpdate.Droid
{
    [Activity(Label = "Winxo Price Update", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestedOrientation = ScreenOrientation.Portrait;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Window.SetStatusBarColor(Color.ParseColor("#96C11F"));
            Window.SetNavigationBarColor(Color.ParseColor("#f2f2f2"));

            //var userSelectedCulture = new CultureInfo("fr-FR");
            //Thread.CurrentThread.CurrentCulture = userSelectedCulture;

            #region Plugins Init
            Acr.UserDialogs.UserDialogs.Init(this);

            #endregion


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}