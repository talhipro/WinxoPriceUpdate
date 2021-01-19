using PropertyChanged;

namespace WinxoPriceUpdate.Shared.Models
{
    [AddINotifyPropertyChangedInterface]
    public class StatutModel
    {
        public string id { get; set; }
        public string statutNom { get; set; }
        public string statutColor { get; set; }
    }
}