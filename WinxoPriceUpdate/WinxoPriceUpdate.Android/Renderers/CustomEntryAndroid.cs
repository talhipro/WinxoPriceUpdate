using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(WinxoPriceUpdate.Controls.CustomEntry), typeof(WinxoPriceUpdate.Droid.Renderers.CustomEntryAndroid))]
namespace WinxoPriceUpdate.Droid.Renderers
{
    public class CustomEntryAndroid : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public CustomEntryAndroid(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            GradientDrawable shape = new GradientDrawable();
            shape.SetColor(Android.Graphics.Color.White);
            shape.SetStroke(2, Color.FromHex("#406680").ToAndroid());
            shape.SetCornerRadius(20);
            Control.SetPadding(30, 40, 30, 40);
            Control.Background = shape;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, Android.Graphics.PorterDuff.Mode.SrcAtop);
        }
    }
}