using System;
using Android.Graphics.Drawables;
using WinxoPriceUpdate.Controls;
using WinxoPriceUpdate.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace WinxoPriceUpdate.Droid.Renderers
{
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {

        public DatePickerCtrlRenderer(Android.Content.Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
                return;

            Control.SetBackgroundResource(Resource.Drawable.StyleEntry);

            Control.SetTextColor(Android.Graphics.Color.ParseColor("#6e6e6e"));
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Control.SetPaddingRelative(20,20,20,20);

            GradientDrawable gd = new GradientDrawable();
            gd.SetCornerRadius(10);
            gd.SetColor(Android.Graphics.Color.Transparent);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#EEEEEE"));

            Control.Background = gd;

            DatePickerCtrl element = Element as DatePickerCtrl;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }
            this.Control.TextChanged += this.Control_TextChanged;
        }

        private void Control_TextChanged(object sender, Android.Text.TextChangedEventArgs arg)
        {
            DatePickerCtrl element = Element as DatePickerCtrl;

            var selectedDate = arg.Text.ToString();
            //if (selectedDate == element.Placeholder)
            //{
            //    Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}

            //var initDate = DateTime.Now.AddDays(1).Date.ToString("dd/MM/yyyy");
            //var initDate = default(DateTime).ToString("dd/MM/yyyy");

            /*if (selectedDate == DateTime.Now.Date.ToString("dd/MM/yyyy"))
            {
                Control.TextChanged -= Control_TextChanged;
                Control.Text = element.PlaceHolder;
                element.IsInitValue = true;
                Control.TextChanged += Control_TextChanged;
            }
            else
            {
                Control.TextChanged -= Control_TextChanged;
                Control.Text = selectedDate;
                element.IsInitValue = false;
                Control.TextChanged += Control_TextChanged;
            }*/


            if (selectedDate != element.Placeholder)
            {
                Control.TextChanged -= Control_TextChanged;
                Control.Text = selectedDate;
                element.IsInitValue = false;
                Control.TextChanged += Control_TextChanged;
            }
        }
    }
}