using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WinxoPriceUpdate.Controls
{
    public class DatePickerCtrl : DatePicker
    {
        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(DatePickerCtrl), defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }

        public static readonly BindableProperty IsInitValueProperty = BindableProperty.Create(propertyName: "IsInitValue", returnType: typeof(bool), declaringType: typeof(DatePickerCtrl), defaultValue: true);
        public bool IsInitValue
        {
            get;
            set;
        } = true;



        /*private string _format = null;
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(propertyName: "NullableDate", returnType: typeof (DateTime?), declaringType: typeof(DatePickerCtrl), defaultValue: null);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue) { if (null != _format) Format = _format; Date = NullableDate.Value; }
            else { _format = Format; Format = "pick ..."; }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Date") NullableDate = Date;
        }*/
    }
}
