using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinxoPriceUpdate.Helpers;
using WinxoPriceUpdate.Shared;
using WinxoPriceUpdate.Shared.Models;
using WinxoPriceUpdate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinxoPriceUpdate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : Base.BasePage
    {
        public HomeViewModel homeViewModel;

        public Home()
        {
            InitializeComponent();

            BindingContext = homeViewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void History_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Historique());
        }

        private async void Panel_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PanelSaisie());

        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();

            await AppHelper.Logout();
        }
    }
}