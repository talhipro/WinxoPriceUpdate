using Android.Content;
using WinxoPriceUpdate.Droid.Renderers;
using WinxoPriceUpdate.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRedered))]
namespace WinxoPriceUpdate.Droid.Renderers
{
    class CustomPickerRedered : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public CustomPickerRedered(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Drawable.PickerStyle);
                Control.SetPadding(Control.PaddingLeft, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
                Control.SetTextSize(Android.Util.ComplexUnitType.Dip, 14);
            }
        }
    }
}