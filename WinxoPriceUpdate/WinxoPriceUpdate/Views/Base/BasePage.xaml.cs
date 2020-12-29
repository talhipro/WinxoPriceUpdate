using WinxoPriceUpdate.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WinxoPriceUpdate.Views.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BasePage : ContentPage
	{
		public BasePage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is BaseViewModel bindingContext) bindingContext.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is BaseViewModel bindingContext) bindingContext.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is BaseViewModel bindingContext) bindingContext.OnBackButtonPressed();

            return base.OnBackButtonPressed();
        }

    }
}