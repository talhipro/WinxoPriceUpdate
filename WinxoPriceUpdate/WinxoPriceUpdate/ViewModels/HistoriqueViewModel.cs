using Shared.httpREST;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Enums;
using WinxoPriceUpdate.Shared.Models;
using Xamarin.Forms;

namespace WinxoPriceUpdate.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class HistoriqueViewModel : Base.BaseViewModel
    {
        public ObservableCollection<DemandeModel> StationDemandes { get; set; } = new ObservableCollection<DemandeModel>();
        public FilterStationDemandesModel FilterStationDemandes { get; set; } = new FilterStationDemandesModel();
        public StatutModel SelectedStatut { get; set; } = new StatutModel();

        public bool IsInitValue { get; set; } = true;
        public int Offset { get; set; } = 0;
        private int _curentPage = 1;
        private int _lastPage = 0;

        private int _filterCurentPage = 1;
        private int _filterLastPage = 0;

        private bool _isLoadinMore = false;
        public bool _isFilterinMore = false;
        private bool _isRefreshing { get; set; }

        public bool ContentVisible { get; set; }
        public bool RefreshBtnVisible { get; set; }

        public Command LoadMoreCommande { get; set; }
        public Command RefreshCommand { get; set; }
        public Command FilterCommande { get; set; }
        public Command CancelFilterCommande { get; set; }
        public ObservableCollection<StatutModel> StatutList { get; set; } = new ObservableCollection<StatutModel>();

        public HistoriqueViewModel ()
        {
            SelectedStatut = new StatutModel();
            LoadMoreCommande = new Command(
                execute: async() =>
                {
                    _isLoadinMore = true;

                    #region Get data from API
                    /*if (FilterStationDemandes.StartDate != null || FilterStationDemandes.EndDate != null || !string.IsNullOrEmpty(FilterStationDemandes.StatutId))
                    {
                        await GetFilteredDemandes(FilterStationDemandes, _filterCurentPage);
                    }
                    else
                    {
                        await GetDemandes(_curentPage + 1);
                    }*/
                    #endregion

                    #region Get data from Local JSON
                    await GetDemandesJsonData();
                    #endregion

                },
                canExecute: () =>
                {
                    return !_isLoadinMore;
                }
            );
            RefreshCommand = new Command(
                execute: async () =>
                {
                    _isRefreshing = true;

                    // Get data from API
                    //await GetDemandes(_curentPage);

                    // Get data from Local JSON
                    await GetDemandesJsonData();
                },
                canExecute: () =>
                {
                    return !_isRefreshing;
                }
            );
            FilterCommande = new Command(
                execute: async () =>
                {
                    return;
                    if (
                        FilterStationDemandes.StartDate == DateTime.Now.Date &&
                        FilterStationDemandes.EndDate == DateTime.Now.Date &&
                        string.IsNullOrEmpty(SelectedStatut?.id))
                    {
                        return;
                    }

                    //if (IsInitValue) return;


                    _isFilterinMore = true;

                    _filterCurentPage = 1;
                    _filterLastPage = 0;

                    if (!string.IsNullOrEmpty(SelectedStatut?.id))
                    {
                        FilterStationDemandes.StatutId = SelectedStatut.id;
                    }

                    await GetFilteredDemandes(FilterStationDemandes, _filterCurentPage);
                },
                canExecute: () =>
                {
                    return !_isFilterinMore;
                }
            );
            CancelFilterCommande = new Command(
                execute: () =>
                {
                    //IsInitValue = true;

                    SelectedStatut = new StatutModel();
                    FilterStationDemandes = new FilterStationDemandesModel();
                }
            );
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            //StatutList.Add(new StatutModel { id = "1", statutNom = "Saisi" });
            //StatutList.Add(new StatutModel { id = "2", statutNom = "Envoyé" });
            //StatutList.Add(new StatutModel { id = "3", statutNom = "Appliqué" });
            //StatutList.Add(new StatutModel { id = "4", statutNom = "Traité" });
            //StatutList.Add(new StatutModel { id = "5", statutNom = "Annulé" });

            await GetStatutsJsonData();

            // Get data from API
            //await GetDemandes(_curentPage);

            // Get data from Local JSON
            await GetDemandesJsonData();
        }

        public async Task GetDemandesJsonData()
        {
            try
            {
                AppHelper.ALoadingShow("Chargement...");

                var RequestResult = await AppHelper.ReadFromJson<ObservableCollection<DemandeModel>>("Assets.MockedData.StationHistorique.json");

                if (RequestResult.success)
                {
                    //StationDemandes = RequestResult.data;
                    foreach (var item in RequestResult.data)
                    {
                        StationDemandes.Add(item);
                    }
                    ContentVisible = true;
                    RefreshBtnVisible = false;
                }
                else
                {
                    if (StationDemandes.Count <= 0)
                    {
                        ContentVisible = false;
                        RefreshBtnVisible = true;
                    }
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

        public async Task GetStatutsJsonData()
        {
            try
            {
                AppHelper.ALoadingShow("Chargement...");

                var RequestResult = await AppHelper.ReadFromJson<ObservableCollection<StatutModel>>("Assets.MockedData.StatutList.json", Encoding.UTF7);

                if (RequestResult.success)
                {

                    StatutList = RequestResult.data;
                    ContentVisible = true;
                    RefreshBtnVisible = false;
                }
                else
                {
                    if (StationDemandes.Count <= 0)
                    {
                        ContentVisible = false;
                        RefreshBtnVisible = true;
                    }
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

                        if (RequestResult.data.data.Count == 0)
                        {
                            RefreshBtnVisible = true;
                            ContentVisible = false;
                        }
                        else
                        {
                            foreach (var item in RequestResult.data.data)
                            {
                                StationDemandes.Add(item);
                            }
                            ContentVisible = true;
                            RefreshBtnVisible = false;
                        }
                    }
                    else
                    {
                        if (StationDemandes.Count <= 0)
                        {
                            ContentVisible = false;
                            RefreshBtnVisible = true;
                        }
                        AppHelper.ASnack(message: RequestResult.message, type: SnackType.Error);
                    }
                }
                else
                {
                    AppHelper.ASnack(message: Assets.Strings.NoMoreResultMessage, type: SnackType.Info);
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

        private async Task GetFilteredDemandes(FilterStationDemandesModel FilterStationDemandes, int page)
        {
            try
            {
                //if (
                //    FilterStationDemandes.StartDate == default &&
                //    FilterStationDemandes.EndDate == default &&
                //    string.IsNullOrEmpty(FilterStationDemandes?.StatutId))
                //{
                //    return;
                //}

                //if (FilterStationDemandes?.StartDate == null && FilterStationDemandes?.EndDate == null && string.IsNullOrEmpty(FilterStationDemandes?.StatutId))


                if (page < _filterLastPage || _filterLastPage == 0)
                {
                    AppHelper.ALoadingShow("Chargement...");

                    var RequestResult = new RESTServiceResponse<PaginateDemandeModel>();
                    if (Offset != 0)
                    {
                        RequestResult = await ApiCalls.FilterStationDemandes(page, FilterStationDemandes, Offset);
                    }
                    else
                    {
                        RequestResult = await ApiCalls.FilterStationDemandes(page, filterStationDemandes: FilterStationDemandes);
                    }

                    if (RequestResult.success)
                    {
                        _filterCurentPage = RequestResult.data.current_page;
                        _filterLastPage = RequestResult.data.last_page;

                        if (_filterCurentPage == 1) StationDemandes.Clear();

                        if (RequestResult.data.data.Count == 0)
                        {
                            RefreshBtnVisible = true;
                            ContentVisible = false;
                        }
                        else
                        {
                            foreach (var item in RequestResult.data.data)
                            {
                                StationDemandes.Add(item);
                            }
                            ContentVisible = true;
                            RefreshBtnVisible = false;
                        }

                        _filterCurentPage++;
                    }
                    else
                    {
                        if (StationDemandes.Count <= 0)
                        {
                            ContentVisible = false;
                            RefreshBtnVisible = true;
                        }
                        AppHelper.ASnack(message: RequestResult.message, type: SnackType.Error);
                    }
                }
                else
                {
                    AppHelper.ASnack(message: Assets.Strings.NoMoreResultMessage, type: SnackType.Info);
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
