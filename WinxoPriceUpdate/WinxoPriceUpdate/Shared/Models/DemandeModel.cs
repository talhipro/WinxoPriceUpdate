using System;
using System.Collections.Generic;

namespace WinxoPriceUpdate.Shared.Models
{


    public class SaveDemandeModel
    {
        public string station_id { get; set; }
        public DateTime dateapplication { get; set; }
        public List<PrixTypeItem> demandePrix { get; set; }
    }

    public class DemandeModel
    {
        public string id { get; set; }
        public string station_id { get; set; }
        //public double gasoilprix { get; set; }
        //public double superprix { get; set; }
        public DateTime datesaisie { get; set; }
        public DateTime dateapplication { get; set; }
        public StatutModel statutRef { get; set; }
        public List<PrixActuel> demandePrix { get; set; }
    }

    public class PaginateDemandeModel
    {
        public int current_page { get; set; }
        public List<DemandeModel> data { get; set; }
        public int last_page { get; set; }
    }
}
