using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Shared.Helpers;
using WinxoPriceUpdate.Shared;
using Xamarin.Forms;

namespace WinxoPriceUpdate.Helpers
{
    public class AppHelper
    {
        #region common Logout
        public static async Task Logout()
        {
            AppSettings.ClearSettings();
            AppSettings.isLoggedIn = false;
            await Task.Delay(100);
            Application.Current.MainPage = new Views.Login();
        }

        #endregion

        public static bool IsLoginValid(LoginModel loginModel)
        {
            if (!string.IsNullOrEmpty(loginModel.UserName) && !string.IsNullOrEmpty(loginModel.Password)
                /*&& loginModel.Username.Length >= 5 && loginModel.Password.Length >= 5*/)
            {
                return true;
            }
            return false;
        }

        public static bool IsTokenStillValid(string TokenValidUntil)
        {
            DateTime ValidUntil;
            var parsedToken = DateTime.TryParse(TokenValidUntil, out ValidUntil);

            if (parsedToken && ValidUntil.Subtract(DateTime.Now).TotalDays > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ALoadingShow(string message)
        {
            UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);
        }
        public static void ALoadingHide()
        {
            UserDialogs.Instance.HideLoading();
        }
        public static string CombineDateTimePickers(DateTime date, TimeSpan time)
        {
            if (date != null && time != null)
            {
                return string.Concat(date.Year, "-", date.Month, "-", date.Day, " ", time.Hours, ":", time.Minutes);
            }
            return string.Empty;
        }

        /*public static async Task<PromptResult> APrompt(string message, string title = "info!", string okText = "OK", string cancelText = "Cancel", string placeholderText = "...")
        {
            return await UserDialogs.Instance.PromptAsync(message, title, okText, cancelText, placeholderText);
        }*/

        /*public static async Task AnAlert(string message, string title = "info!", string okText = "OK", Action action = null, bool useFormsAlert = true)
        {
            if (useFormsAlert)
            {
                var currentPage = Xamarin.Forms.Application.Current?.MainPage;
                if (currentPage != null)
                {
                    await currentPage.DisplayAlert(title, message, okText);
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(new AlertConfig()
                {
                    Title = title,
                    Message = message,
                    OkText = okText,
                    OnAction = action,
                });
            }

        }*/

        /*public static IDisposable ASnack(string message, int durationInMs = 4000)
        {
            return UserDialogs.Instance.Toast(new ToastConfig(message)
            {
                Duration = TimeSpan.FromMilliseconds(durationInMs),
                BackgroundColor = System.Drawing.Color.FromArgb(0, 0, 0)
            });
        }*/

        /*public static void AToast(string message, bool isLongAlert = false)
        {
            if (!isLongAlert)
                DependencyService.Get<IToast>().ShortAlert(message);
            else
                DependencyService.Get<IToast>().LongAlert(message);


        }*/

        /*public static View ADisableView(View view)
        {
            view.IsEnabled = false;
            return view;
        }*/

        /*public static View AEnableView(View view)
        {
            view.IsEnabled = true;
            return view;
        }*/

        /*public static List<View> ADisableViews(List<View> views)
        {
            views.ForEach(view =>
            {
                view.IsEnabled = false;
            });
            return views;
        }*/

        /*public static List<View> AEnableViews(List<View> views)
        {
            views.ForEach(view =>
            {
                view.IsEnabled = true;
            });
            return views;
        }*/
    }
}
