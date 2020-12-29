using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Models;
using WinxoPriceUpdate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinxoPriceUpdate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historique : Base.BasePage
    {
        public HistoriqueViewModel historiqueViewModel;
        public Historique()
        {
            InitializeComponent();

            BindingContext = historiqueViewModel = new HistoriqueViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();

            await Helpers.AppHelper.Logout();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}