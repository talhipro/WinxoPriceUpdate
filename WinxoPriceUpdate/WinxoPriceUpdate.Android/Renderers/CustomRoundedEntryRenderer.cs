[assembly: Xamarin.Forms.ExportRenderer(typeof(WinxoPriceUpdate.Controls.CustomRoundedEntry), typeof(WinxoPriceUpdate.Droid.Renderers.CustomRoundedEntryRenderer))]
namespace WinxoPriceUpdate.Droid.Renderers
{
    public class CustomRoundedEntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public CustomRoundedEntryRenderer(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);


            if (Control == null || e.NewElement == null) return;


            Control.SetBackgroundResource(Resource.Drawable.StyleEntry);
            Control.SetPadding(15, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            Control.Gravity = Android.Views.GravityFlags.CenterVertical;
        }
    }
}