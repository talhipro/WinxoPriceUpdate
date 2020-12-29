using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shared.httpREST;
using Shared.Helpers;
using WinxoPriceUpdate.Helpers;

namespace WinxoPriceUpdate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public LoginModel LoginInputs { get; set; }
        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Home());

            return;

            try
            {
                LoginInputs = new LoginModel
                {
                    UserName = UserName.Text,
                    Password = Password.Text
                };

                if (Helpers.AppHelper.IsLoginValid(LoginInputs))
                {
                    #region Web Service Call with activity indicator
                    AppHelper.ALoadingShow("Connexion...");

                    var loginResult = await ApiCalls.Login(LoginInputs);
                    #endregion

                    #region web service response is null or error

                    if (!string.IsNullOrWhiteSpace(loginResult?.Error))
                    {
                        if (!string.IsNullOrWhiteSpace(loginResult?.ErrorDescription))
                        {
                            //AppHelper.AToast(loginResult?.ErrorDescription); return;
                            await App.Current.MainPage.DisplayAlert("Erreur de login", loginResult?.ErrorDescription, "ok");
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(loginResult?.Error))
                        {
                            //AppHelper.AToast(loginResult?.Error); return;
                            await App.Current.MainPage.DisplayAlert("Erreur de login", loginResult?.Error, "ok");
                            return;
                        }
                    }
                    #endregion

                    #region is generated token valid
                    //var isTokenValid = Helperino.AAreInputsNullOrEmpty(new string[] { loginResult.AccessToken });
                    if (string.IsNullOrWhiteSpace(loginResult?.AccessToken) || string.IsNullOrEmpty(loginResult?.AccessToken))
                    {
                        await App.Current.MainPage.DisplayAlert("Erreur de login", $"Invalid token", "ok");
                        return;
                    }
                    #endregion

                    #region Fill user settings info
                    //AppSettings.RefreshToken = loginResult.RefreshToken;
                    AppSettings.AccessToken = loginResult.AccessToken;
                    AppSettings.UserId = loginResult.UserId;
                    AppSettings.ValidUntil = DateTime.Now.AddSeconds(loginResult.ExpiresIn).ToString();
                    AppSettings.FullName = loginResult.FullName;
                    AppSettings.UserRoles = loginResult.UserRoles;
                    AppSettings.Email = loginResult.email;
                    AppSettings.Phone = loginResult.phone;
                    #endregion

                    #region Set isLoggedIn variable to true & go to homepage
                    AppSettings.isLoggedIn = true;
                    Application.Current.MainPage = new NavigationPage(new Home());
                    #endregion
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erreur de login", ex.Message, "ok");
            }
            finally
            {
                AppHelper.ALoadingHide();
            }




            //Application.Current.MainPage = new NavigationPage(new Home());
        }
    }
}