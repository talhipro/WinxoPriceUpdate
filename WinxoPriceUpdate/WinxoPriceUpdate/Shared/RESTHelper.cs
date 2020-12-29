using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Shared;

namespace Shared.httpREST
{
    public class RESTHelper
    {
        #region Login Method
        public static async Task<LoginResultModel> PostLoginRequest(LoginModel loginModel, string acceptMediaType = "application/json", string fromKey = "")
        {
            try
            {
                #region Is valid model
                if (loginModel == null || string.IsNullOrWhiteSpace(loginModel?.UserName) || string.IsNullOrWhiteSpace(loginModel?.Password))
                    return new LoginResultModel() { Error = "Invalid input", ErrorDescription = "Invalid username/ password" };
                #endregion

                #region IsConnected
                if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    return new LoginResultModel() { Error = "Connection problem", ErrorDescription = "No connection available!" };
                #endregion

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AppUrls.Login);
                    client.DefaultRequestHeaders.Accept.Clear();


                    if (!string.IsNullOrWhiteSpace(acceptMediaType))
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptMediaType));

                    if (!string.IsNullOrWhiteSpace(fromKey))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("From", fromKey);

                    var formEncodedContent = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("grant_type", "password"),
                         new KeyValuePair<string, string>("email", loginModel.UserName),
                         new KeyValuePair<string, string>("password", loginModel.Password),
                    });


                    var content = new StringContent(JsonConvert.SerializeObject(loginModel).ToString(), Encoding.UTF8, "application/json");
                    //var responseMessage = await client.PostAsync(uri, content);

                    var responseMessage = await client.PostAsync(AppUrls.Login, formEncodedContent);

                    var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                    LoginResultModel result = JsonConvert.DeserializeObject<LoginResultModel>(jsonResponse);

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new LoginResultModel() { Error = "Error", ErrorDescription = ex.Message };
            }
        }
        #endregion

        #region GetRequest GetParams as NameValueCollection
        public static async Task<RESTServiceResponse<T>> GetRequest<T>(string token, string url, HttpVerbs method = HttpVerbs.GET, NameValueCollection getParams = null, object postObject = null, string contentType = "application/json")
        {
            try
            {
                #region IsConnected

                bool IsConnected = Plugin.Connectivity.CrossConnectivity.Current.IsConnected;
                if (!IsConnected)
                {
                    return new RESTServiceResponse<T>(false, "Vous n'êtes pas connéctés !");
                }
                #endregion


                using (var client = new HttpClient())
                {
                    //setup client
                    Uri uri = new Uri(url);
                    #region Setting Attachements

                    if (method == HttpVerbs.GET && getParams != null)
                    {
                        uri = uri.AttachParameters(getParams);
                    }

                    #endregion
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    //make request
                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (method)
                    {
                        case HttpVerbs.GET:
                            try
                            {
                                response = await client.GetAsync(uri);
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        case HttpVerbs.POST:
                            try
                            {
                                var content = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, contentType);
                                response = await client.PostAsync(uri, content);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            break;
                    }

                    var stringResponseJson = await response.Content?.ReadAsStringAsync();

                    /*if (!string.IsNullOrEmpty(datetimeformat) && !string.IsNullOrWhiteSpace(datetimeformat))
                    {
                        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformat };
                        RESTServiceResponse<T> result = JsonConvert.DeserializeObject<RESTServiceResponse<T>>(stringResponseJson, dateTimeConverter);
                        return result;
                    }
                    else
                    {*/
                        RESTServiceResponse<T> result = JsonConvert.DeserializeObject<RESTServiceResponse<T>>(stringResponseJson);
                        return result;
                    //}
                }
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<T>(false, ex.Message);
            }
        }
        #endregion
    }

    public static class RESTExtensions
    {
        #region Attach "NameValueCollection" Parameters
        public static Uri AttachParameters(this Uri uri, NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();
            string str = "?";
            for (int index = 0; index < parameters.Count; ++index)
            {
                stringBuilder.Append(str + parameters.AllKeys[index] + "=" + parameters[index]);
                str = "&";
            }
            return new Uri(uri + stringBuilder.ToString());
        }
        #endregion
    }
}
