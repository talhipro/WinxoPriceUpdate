using Shared.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinxoPriceUpdate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new Views.Home());
            MainPage = new NavigationPage(new Views.Login());

            //var token = AppSettings.AccessToken;
            /*bool isTokenValid = Helpers.AppHelper.IsTokenStillValid(AppSettings.ValidUntil);

            if (isTokenValid)
            {
                MainPage = new NavigationPage(new Views.Home());
                return;
            }
            else
            {
                MainPage = new Views.Login();
            }*/
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
