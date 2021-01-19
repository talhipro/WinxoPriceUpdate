using Shared;
using Shared.Helpers;
using Shared.httpREST;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Shared.Models;

namespace WinxoPriceUpdate.Shared
{
    public static class ApiCalls
    {
        #region 1 - Login
        public static async Task<LoginResultModel> Login(LoginModel model)
        {
            var apiUrl = $"{AppUrls.Login}";
            return await RESTHelper.PostLoginRequest(model);
        }
        #endregion

        #region 2 - Get Station Details
        public static async Task<RESTServiceResponse<StationModel>> GetStationDetails()
        {
            try
            {
                return await RESTHelper.GetRequest<StationModel>(url: AppUrls.StationDetails, token: AppSettings.AccessToken, method: HttpVerbs.GET);
            }
            catch (Exception ex)
            {
                //return new RESTServiceResponse<StationModel>() { success = false, message = $"Erreur: {ex.Message}" };
                return new RESTServiceResponse<StationModel>() { success = false, message = Assets.Strings.ErreurMessage };
            }
        }
        #endregion

        #region 3 - Get Station Demandes
        public static async Task<RESTServiceResponse<PaginateDemandeModel>> GetStationDemandes(int page, int offset = 0)
        {
            try
            {
                // /stationDemandes/1?page=1

                string url = string.Concat(AppUrls.StationDemandes, 1/*, AppSettings.UserId*/, "?page=" + page, (offset != 0) ? "&offset=" + offset : "");

                return await RESTHelper.GetRequest<PaginateDemandeModel>(url: url, token: AppSettings.AccessToken, method: HttpVerbs.GET);
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<PaginateDemandeModel>() { success = false, message = $"Erreur: {ex.Message}" };
            }
        }
        #endregion

        #region 4 - Post new Demande
        public static async Task<RESTServiceResponse<DemandeModel>> SaveNewDemande(SaveDemandeModel model)
        {
            try
            {
                return await RESTHelper.GetRequest<DemandeModel>(url: AppUrls.NewDemande, token: AppSettings.AccessToken, method: HttpVerbs.POST, postObject: model);
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<DemandeModel>() { success = false, message = Assets.Strings.ErreurMessage };
            }
        }
        #endregion

        #region 5 - Get All Prix Types
        public static async Task<RESTServiceResponse<ObservableCollection<PrixTypeForm>>> GetPrixTypes()
        {
            try
            {
                return await RESTHelper.GetRequest<ObservableCollection<PrixTypeForm>>(url: AppUrls.GetPriceTypes, token: AppSettings.AccessToken, method: HttpVerbs.GET);
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<ObservableCollection<PrixTypeForm>>() { success = false, message = Assets.Strings.ErreurMessage };
            }
        }
        #endregion

        #region 6 - Get Filtered Station Demandes
        public static async Task<RESTServiceResponse<PaginateDemandeModel>> FilterStationDemandes(int page, FilterStationDemandesModel filterStationDemandes, int offset = 0)
        {
            try
            {
                string url = string.Concat(AppUrls.FilterStationDemandes, 1/*, AppSettings.UserId*/, "?page=" + page, (offset != 0) ? "&offset=" + offset : "");

                return await RESTHelper.GetRequest<PaginateDemandeModel>(url: url, token: AppSettings.AccessToken, method: HttpVerbs.POST, postObject: filterStationDemandes);
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<PaginateDemandeModel>() { success = false, message = $"Erreur: {ex.Message}" };
            }
        }
        #endregion


        #region - Get Station List
        public static async Task<RESTServiceResponse<ObservableCollection<StationModel>>> GetStationList()
        {
            try
            {
                return await RESTHelper.GetRequest<ObservableCollection<StationModel>>(url: AppUrls.StationList, token: AppSettings.AccessToken, method: HttpVerbs.GET);
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<ObservableCollection<StationModel>>() { success = false, message = Assets.Strings.ErreurMessage };
            }
        }
        #endregion
    }
}
