using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Models;
using WinxoPriceUpdate.Shared.Enums;
using Xamarin.Forms;

namespace WinxoPriceUpdate.ViewModels
{
    public class PanelSaisieViewModel : Base.BaseViewModel
    {
        public SaveDemandeModel DemandeInputs { get; set; }
        public ObservableCollection<PrixTypeForm> PrixTypes { get; set; } = new ObservableCollection<PrixTypeForm>();
        public DateTime? DateInput { get; set; } = null;
        public TimeSpan TimeInput { get; set; } = new TimeSpan(DateTime.Now.Hour + 1, DateTime.Now.Minute, DateTime.Now.Second);

        public Command SubmitFormCommande { get; set; }
        public bool IsSubmitingForm { get; set; }

        public PanelSaisieViewModel()
        {
            DemandeInputs = new SaveDemandeModel
            {
                station_id = "1"/*AppSettings.UserId*/,
                demandePrix = new List<PrixTypeItem>(),
            };

            SubmitFormCommande = new Command(
                execute: async () =>
                {
                    IsSubmitingForm = true;

                    await OnSubmitForm();
                },
                canExecute: () =>
                {
                    return !IsSubmitingForm;
                }
            );

        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            // Get data from API
            //await GetPrixTypes();

            // Get data from Local JSON
            await GetPrixTypesJsonData();
        }

        public async Task GetPrixTypesJsonData()
        {
            try
            {
                AppHelper.ALoadingShow("Chargement...");

                var RequestResult = await AppHelper.ReadFromJson<ObservableCollection<PrixTypeForm>>("Assets.MockedData.PriceTypes.json");

                if (RequestResult.success)
                {
                    PrixTypes = RequestResult.data;
                }
                else
                {
                    AppHelper.ASnack(message: RequestResult.message, type: SnackType.Error);
                    await Application.Current.MainPage.Navigation.PopAsync();
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

        private async Task GetPrixTypes()
        {
            try
            {
                AppHelper.ALoadingShow("Chargement...");
                var RequestResult = await ApiCalls.GetPrixTypes();
                if (RequestResult.success)
                {
                    PrixTypes = RequestResult.data;
                }
                else
                {
                    AppHelper.ASnack(message: RequestResult.message, type: SnackType.Error);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception)
            {
                AppHelper.ASnack(message: Assets.Strings.ErreurMessage, type: SnackType.Error);
            }
            finally
            {
                AppHelper.ALoadingHide();
            }
        }

        private async Task OnSubmitForm()
        {
            // Date Format to send 2020-12-04 15:10
            try
            {
                if (PrixTypes.Count == 0)
                {
                    return;
                }
                bool _priceValidation = false;
                foreach (var prix in PrixTypes)
                {
                    if (!(string.IsNullOrEmpty(prix.montant) || double.Parse(prix.montant) == 0))
                    {
                        _priceValidation = true;
                        break;
                    }
                }
                if(!_priceValidation)
                {
                    AppHelper.ASnack(message: Assets.Strings.ErreurEmptyPriceMessage, type: SnackType.Warning);
                    return;
                }
                if(!DateInput.HasValue)
                {
                    AppHelper.ASnack(message: Assets.Strings.ErreurDateApplicationEmpty, type: SnackType.Warning);
                    return;
                }

                AppHelper.ALoadingShow("Chargement...");

                DateTime FinalApplicationDateTime = new DateTime(((DateTime)DateInput).Year, ((DateTime)DateInput).Month, ((DateTime)DateInput).Day, TimeInput.Hours, TimeInput.Minutes, TimeInput.Seconds);

                DemandeInputs.dateapplication = FinalApplicationDateTime;

                foreach (var prix in PrixTypes)
                {
                    if (prix?.montant != null)
                    {
                        DemandeInputs.demandePrix.Add(new PrixTypeItem
                        {
                            prixType_id = prix.id,
                            montant = double.Parse(prix.montant)
                        });
                    }
                }

                // Post Demande to API
                //var RequestResult = await ApiCalls.SaveNewDemande(DemandeInputs);

                // Get data from Local JSON
                var RequestResult = await AppHelper.ReadFromJson<DemandeModel>("Assets.MockedData.AddNewDemandeResponse.json");

                if (RequestResult.success)
                {
                    //string jsonString = JsonConvert.SerializeObject(RequestResult.data, Formatting.Indented);
                    //await Application.Current.MainPage.DisplayAlert("Data", jsonString, "Ok");
                    AppHelper.ASnack(message: RequestResult.message, type: SnackType.Success);

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
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
