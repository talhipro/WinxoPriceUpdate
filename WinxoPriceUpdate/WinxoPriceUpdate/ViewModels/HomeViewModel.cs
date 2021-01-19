using Newtonsoft.Json;
using Shared.httpREST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Enums;
using WinxoPriceUpdate.Shared.Models;
using Xamarin.Forms;

namespace WinxoPriceUpdate.ViewModels
{
    public class HomeViewModel : Base.BaseViewModel
    {
        public StationModel StationDetails { get; set; }
        public bool ContentVisible { get; set; }
        public bool RefreshBtnVisible { get; set; }
        private bool _isRefreshing { get; set; }

        public Command RefreshCommand { get; set; }

        public HomeViewModel()
        {
            RefreshCommand = new Command(
                execute: async () =>
                {
                    _isRefreshing = true;

                    // Get data from API
                    //await GetStationDetails();

                    // Get data from Local JSON
                    await GetJsonData();
                },
                canExecute: () =>
                {
                    return !_isRefreshing;
                }
            );
        }
        public async override void OnAppearing()
        {
            base.OnAppearing();

            // Get data from API
            //await GetStationDetails();

            // Get data from Local JSON
            await GetJsonData();
        }

        public async Task GetJsonData()
        {
            try
            {
                AppHelper.ALoadingShow("Chargement...");

                var RequestResult = await AppHelper.ReadFromJson<StationModel>("Assets.MockedData.StationDetails.json");

                if (RequestResult.success)
                {
                    StationDetails = RequestResult.data;
                    ContentVisible = true;
                    RefreshBtnVisible = false;
                }
                else
                {
                    ContentVisible = false;
                    RefreshBtnVisible = true;
                    AppHelper.ASnack(message: RequestResult.message, type: SnackType.Error);
                }
            }
            catch (Exception ex)
            {
                AppHelper.ASnack(message: Assets.Strings.ErreurMessage, type: SnackType.Error);
            }
            finally
            {
                AppHelper.ALoadingHide();
            }
        }

        private async Task GetStationDetails()
        {
            try
            {
                AppHelper.ALoadingShow("Chargement...");
                var RequestResult = await ApiCalls.GetStationDetails();

                if (RequestResult.success)
                {
                    StationDetails = RequestResult.data;
                    ContentVisible = true;
                    RefreshBtnVisible = false;
                }
                else
                {
                    ContentVisible = false;
                    RefreshBtnVisible = true;
                    AppHelper.ASnack(message: RequestResult.message, type: SnackType.Error);
                }
            }
            catch (Exception ex)
            {
                AppHelper.ASnack(message: Assets.Strings.ErreurMessage, type: SnackType.Error);
            }
            finally
            {
                AppHelper.ALoadingHide();
            }
        }
    }
}
