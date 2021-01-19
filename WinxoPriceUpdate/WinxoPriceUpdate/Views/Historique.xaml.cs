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
    [PropertyChanged.AddINotifyPropertyChangedInterface]
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

        private void Item_Tapped(object sender, EventArgs e)
        {
            var ItemContext = ((StackLayout)sender).BindingContext as DemandeModel;



            if (ItemContext.DetailsColapse)
            {
                ItemContext.DetailsColapse = false;
            }
            else
            {
                ItemContext.DetailsColapse = true;
            }
        }

        private async void FilterTitle_Tapped(object sender, EventArgs e)
        {
            FilterFields.IsVisible = !FilterFields.IsVisible;

            if (FilterFields.IsVisible)
                await ArrowImg.RotateTo(180, 0, easing: Easing.SinIn);
            else
                await ArrowImg.RotateTo(0, 0, easing: Easing.SinIn);
        }

        private void DatePickerCtrl_DateSelected(object sender, DateChangedEventArgs e)
        {
            //var _selectedDate = ((Controls.DatePickerCtrl)sender).Date;
            //var _dateNow = DateTime.Now.Date;
            //if (historiqueViewModel.IsInitValue && _selectedDate == _dateNow)
            //{
            //    ((Controls.DatePickerCtrl)sender).IsInitValue = true;
            //}
            //else
            //{
            //    ((Controls.DatePickerCtrl)sender).IsInitValue = false;
            //    historiqueViewModel.IsInitValue = false;
            //}
        }

        private void Statut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Picker)sender).SelectedIndex != -1)
                historiqueViewModel.IsInitValue = false;
        }
    }
}