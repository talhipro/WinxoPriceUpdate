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

            //var token = AppSettings.AccessToken;
            bool isTokenValid = Helpers.AppHelper.IsTokenStillValid(AppSettings.ValidUntil);

            if (isTokenValid)
            {
                //Create the master page & give it a default page. check for token's validity tho

                MainPage = new NavigationPage(new Views.Home());
                return;
            }
            else
            {
                MainPage = new Views.Login();
            }
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
