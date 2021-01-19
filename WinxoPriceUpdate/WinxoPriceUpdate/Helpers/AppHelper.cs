using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Shared.Helpers;
using Shared.httpREST;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Enums;
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
            UserDialogs.Instance.ShowLoading(message);
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

        public static async Task<PromptResult> APrompt(string message, string title = "info!", string okText = "OK", string cancelText = "Cancel", string placeholderText = "...")
        {
            return await UserDialogs.Instance.PromptAsync(message, title, okText, cancelText, placeholderText);
        }

        public static async Task AnAlert(string message, string title = "info!", string okText = "OK", Action action = null, bool useFormsAlert = true)
        {
            if (useFormsAlert)
            {
                var currentPage = Application.Current?.MainPage;
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

        }

        public static IDisposable ASnack(string message, int durationInMs = 4, SnackType type = SnackType.Info)
        {
            Color bgColor = Color.White;
            Color textColor = Color.White;
            switch (type)
            {
                case SnackType.Info:
                    bgColor = Color.FromHex("#98c6dd");
                    textColor = Color.FromHex("#31708f");
                    break;
                case SnackType.Error:
                    bgColor = Color.FromHex("#FFBABA");
                    textColor = Color.FromHex("#D8000C");
                    break;
                case SnackType.Success:
                    bgColor = Color.FromHex("#DFF2BF");
                    textColor = Color.FromHex("#4F8A10");
                    break;
                case SnackType.Warning:
                    bgColor = Color.FromHex("#FEEFB3");
                    textColor = Color.FromHex("#9F6000");
                    break;
                default:
                    break;
            }

            return UserDialogs.Instance.Toast(new ToastConfig(message)
            {
                Duration = TimeSpan.FromSeconds(durationInMs),
                BackgroundColor = bgColor,
                MessageTextColor = textColor,
                Position = ToastPosition.Bottom
            });
        }

        public static Task<RESTServiceResponse<T>> ReadFromJson<T>(string jsonFileName, Encoding encoding = null)
        {
            try
            {
                //string jsonFileName = "Assets.TestJsonFile.json";
                //RESTServiceResponse<T> result = new RESTServiceResponse<T>();

                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");

                if (stream != null)
                {
                    var encodage = (encoding == null) ? Encoding.UTF8 : encoding;
                    using (var reader = new StreamReader(stream, encodage))
                    {
                        var jsonString = reader.ReadToEnd();

                        //Converting JSON Array Objects into generic list
                        RESTServiceResponse<T> result = JsonConvert.DeserializeObject<RESTServiceResponse<T>>(jsonString);
                        return Task.FromResult(result);
                    }
                }
                else
                {
                    return Task.FromResult(new RESTServiceResponse<T>(false, Assets.Strings.ErreurMessage));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new RESTServiceResponse<T>(false, Assets.Strings.ErreurMessage));
            }
        }

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
