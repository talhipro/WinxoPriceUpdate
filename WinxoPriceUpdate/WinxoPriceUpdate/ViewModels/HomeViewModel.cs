using System;
using System.Collections.Generic;
using System.Text;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Models;

namespace WinxoPriceUpdate.ViewModels
{
    public class HomeViewModel : Base.BaseViewModel
    {
        public StationModel StationDetails { get; set; }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                AppHelper.ALoadingShow("Chargement...");
                var RequestResult = await ApiCalls.GetStationDetails();

                if (RequestResult.success)
                {
                    StationDetails = RequestResult.data;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erreur", RequestResult.message, "ok");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erreur", ex.Message, "ok");
            }
            finally
            {
                AppHelper.ALoadingHide();
            }
        }
    }
}
