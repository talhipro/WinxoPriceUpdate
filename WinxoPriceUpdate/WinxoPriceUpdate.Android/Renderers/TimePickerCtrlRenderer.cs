using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using WinxoPriceUpdate.Controls;
using WinxoPriceUpdate.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TimePickerCtrl), typeof(TimePickerCtrlRenderer))]
namespace WinxoPriceUpdate.Droid.Renderers
{
    public class TimePickerCtrlRenderer : TimePickerRenderer
    {
        public TimePickerCtrlRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            Control.SetBackgroundResource(Resource.Drawable.StyleEntry);

            Control.SetTextColor(Android.Graphics.Color.ParseColor("#6e6e6e"));
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Control.SetPaddingRelative(20, 20, 20, 20);

            GradientDrawable gd = new GradientDrawable();
            gd.SetCornerRadius(10);
            gd.SetColor(Android.Graphics.Color.Transparent);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#EEEEEE"));

            Control.Background = gd;

            TimePickerCtrl element = Element as TimePickerCtrl;

            this.Control.TextChanged += (sender, arg) =>
            {
                var selectedDate = arg.Text.ToString();
            };
        }
    }
}