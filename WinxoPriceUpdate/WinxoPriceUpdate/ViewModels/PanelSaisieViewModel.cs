using Newtonsoft.Json;
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
    public class PanelSaisieViewModel : Base.BaseViewModel
    {
        public SaveDemandeModel DemandeInputs { get; set; }
        public ObservableCollection<PrixTypeForm> PrixTypes { get; set; } = new ObservableCollection<PrixTypeForm>();
        public DateTime DateInput { get; set; } = DateTime.Now;
        public TimeSpan TimeInput { get; set; } = new TimeSpan(DateTime.Now.Hour + 1, DateTime.Now.Minute, DateTime.Now.Second);

        public Command SubmitFormCommande { get; set; }
        private bool IsSubmitingForm = false;

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

            var RequestResult = await ApiCalls.GetPrixTypes();
            if (RequestResult.success)
            {
                PrixTypes = RequestResult.data;
            }
        }

        private async Task OnSubmitForm()
        {
            // Date Format to send 2020-12-04 15:10
            try
            {
                //if (DemandeInputs.gasoilprix == 0 && DemandeInputs.superprix == 0)
                //{
                //    await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez mentionner les prix", "Ok");
                //    return;
                //}
                AppHelper.ALoadingShow("Chargement...");

                DateTime FinalApplicationDateTime = new DateTime(DateInput.Year, DateInput.Month, DateInput.Day, TimeInput.Hours, TimeInput.Minutes, TimeInput.Seconds);

                DemandeInputs.dateapplication = FinalApplicationDateTime;

                foreach (var prix in PrixTypes)
                {
                    DemandeInputs.demandePrix.Add(new PrixTypeItem
                    {
                        prixType_id = prix.id,
                        montant = double.Parse(prix.montant/*.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture*/)
                    });
                }

                var RequestResult = await ApiCalls.SaveNewDemande(DemandeInputs);

                if (RequestResult.success)
                {
                    string jsonString = JsonConvert.SerializeObject(RequestResult.data, Formatting.Indented);

                    await Application.Current.MainPage.DisplayAlert("succès", RequestResult.message, "Ok");
                    await Application.Current.MainPage.DisplayAlert("Data", jsonString, "Ok");

                    await Application.Current.MainPage.Navigation.PopAsync();
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
