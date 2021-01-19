using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace WinxoPriceUpdate.Shared.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ListPrix
    {
        public int id { get; set; }
        public string prix_winxo_id { get; set; }
        public string type { get; set; }
        public decimal prix { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class PrixVenteWinxo
    {
        public string date { get; set; }
        public List<ListPrix> listPrix { get; set; }
    }

    //public class PrixType
    //{
    //    public int id { get; set; }
    //    public string type { get; set; }
    //    public string nom { get; set; }
    //}

    [AddINotifyPropertyChangedInterface]
    public class PrixTypeForm
    {
        public int id { get; set; }
        public string type { get; set; }
        public string nom { get; set; }
        public string montant { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class PrixActuel
    {
        public int id { get; set; }
        public string type { get; set; }
        public string nom { get; set; }
        public int demande_id { get; set; }
        public int prixType_id { get; set; }
        public decimal montant { get; set; }
    }

    [AddINotifyPropertyChangedInterface]
    public class PrixTypeItem
    {
        public int prixType_id { get; set; }
        public double montant { get; set; }
    }

}
