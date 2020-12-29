[assembly: Xamarin.Forms.ExportRenderer(typeof(WinxoPriceUpdate.Controls.CustomRoundedEditor), typeof(WinxoPriceUpdate.Droid.Renderers.CustomRoundedEditorRenderer))]
namespace WinxoPriceUpdate.Droid.Renderers
{
    public class CustomRoundedEditorRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public CustomRoundedEditorRenderer(Android.Content.Context context) : base(context)
        { }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            Control.SetBackgroundResource(Resource.Drawable.StyleEntry);
            Control.SetPadding(15, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            /*Control.SetMinimumHeight(System.Convert.ToInt32(Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, 45, Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Resources.DisplayMetrics)));*/

        }
    }
}