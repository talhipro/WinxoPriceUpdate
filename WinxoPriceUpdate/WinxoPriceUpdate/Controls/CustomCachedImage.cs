namespace Mobile.Controls
{
    //public class CustomCachedImage : FFImageLoading.Forms.CachedImage
    //{
    //    public static Xamarin.Forms.BindableProperty TintColorProperty = Xamarin.Forms.BindableProperty.Create(nameof(TintColor), typeof(Xamarin.Forms.Color), typeof(CustomCachedImage), Xamarin.Forms.Color.Transparent, propertyChanged: UpdateColor);

    //    public Xamarin.Forms.Color TintColor
    //    {
    //        get { return (Xamarin.Forms.Color)GetValue(TintColorProperty); }
    //        set { SetValue(TintColorProperty, value); }
    //    }

    //    private static void UpdateColor(Xamarin.Forms.BindableObject bindable, object oldColor, object newColor)
    //    {

    //        if (newColor == null || oldColor == null)
    //        {
    //            return;
    //        }

    //        var oldcolor = (Xamarin.Forms.Color)oldColor;
    //        var newcolor = (Xamarin.Forms.Color)newColor;



    //        if (!oldcolor.Equals(newcolor))
    //        {
    //            var view = (CustomCachedImage)bindable;
    //            var transformations = new System.Collections.Generic.List<FFImageLoading.Work.ITransformation>() {
    //                new FFImageLoading.Transformations.TintTransformation((int)(newcolor.R * 255), (int)(newcolor.G * 255), (int)(newcolor.B * 255), (int)(newcolor.A * 255)) {
    //                    EnableSolidColor = true
    //                }
    //            };
    //            view.Transformations = transformations;
    //        }
    //    }
    //}
}
