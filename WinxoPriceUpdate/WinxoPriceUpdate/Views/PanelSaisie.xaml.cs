using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using WinxoPriceUpdate.ViewModels;

namespace WinxoPriceUpdate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanelSaisie : Base.BasePage
    {
        public PanelSaisieViewModel panelSaisieViewModel;
        public PanelSaisie()
        {
            InitializeComponent();

            BindingContext = panelSaisieViewModel = new PanelSaisieViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();

            await Helpers.AppHelper.Logout();
        }

        private void CustomRoundedEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var entry = (Entry)sender;
            //entry.TextChanged -= CustomRoundedEntry_TextChanged;

            //if (entry.Text.Contains(",") && (e.NewTextValue != null && e.NewTextValue.EndsWith(".")))
            //{
            //    entry.Text = e.OldTextValue;
            //}
            //else
            //{
            //    entry.Text = e.NewTextValue?.Replace(".", ",");
            //}

            //entry.TextChanged += CustomRoundedEntry_TextChanged;
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}