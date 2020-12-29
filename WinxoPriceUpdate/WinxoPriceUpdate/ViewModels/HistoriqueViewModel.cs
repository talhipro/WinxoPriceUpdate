using Shared.httpREST;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Models;
using Xamarin.Forms;

namespace WinxoPriceUpdate.ViewModels
{
    public class HistoriqueViewModel : Base.BaseViewModel
    {
        public ObservableCollection<DemandeModel> StationDemandes { get; set; } = new ObservableCollection<DemandeModel>();
        public int Offset { get; set; } = 0;
        private int _curentPage = 1;
        private int _lastPage = 0;
        private bool IsLoadinMore = false;

        public Command LoadMoreCommande { get; set; }

        public HistoriqueViewModel ()
        {
            LoadMoreCommande = new Command(
                execute: async() =>
                {
                    IsLoadinMore = true;

                    await GetDemandes(_curentPage + 1);
                },
                canExecute: () =>
                {
                    return !IsLoadinMore;
                }
            );
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            await GetDemandes(_curentPage);
        }

        private async Task GetDemandes(int page)
        {
            try
            {
                if (page < _lastPage || _lastPage == 0)
                {
                    AppHelper.ALoadingShow("Chargement...");

                    var RequestResult = new RESTServiceResponse<PaginateDemandeModel>();
                    if (Offset != 0)
                    {
                        RequestResult = await ApiCalls.GetStationDemandes(page, Offset);
                    }
                    else
                    {
                        RequestResult = await ApiCalls.GetStationDemandes(page);
                    }

                    if (RequestResult.success)
                    {
                        _curentPage = RequestResult.data.current_page;
                        _lastPage = RequestResult.data.last_page;

                        foreach (var item in RequestResult.data.data)
                        {
                            StationDemandes.Add(item);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Erreur", RequestResult.message, "ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "No more data to show!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", ex.Message, "ok");
            }
            finally
            {
                AppHelper.ALoadingHide();
            }
        }

    }
}
