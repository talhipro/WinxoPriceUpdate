using PropertyChanged;
using System.Collections.Generic;

namespace WinxoPriceUpdate.Shared.Models
{
    [AddINotifyPropertyChangedInterface]
    public class StationModel
    {
        public string id { get; set; }
        public string nom { get; set; }
        public string ville { get; set; }
        public string code { get; set; }
        //public double gasoilprix { get; set; }
        //public double superprix { get; set; }
        public List<PrixVenteWinxo> PrixVenteWinxo { get; set; }
        public List<PrixActuel> PrixActuels { get; set; }
    }
}
