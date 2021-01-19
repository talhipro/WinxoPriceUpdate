namespace Shared
{
    public static class AppUrls
    {
        public const string BaseUrl = "http://192.168.11.108/winxoapi/public/api";

        public const string Login = BaseUrl + "/token";

        public const string StationList = BaseUrl + "/station/station_list";
        //public const string StationDetails = BaseUrl + "station/station_details";
        //public const string StationDemandes = BaseUrl + "station_demandes/";
        //public const string NewDemande = BaseUrl + "demande";


        public const string StationDetails = BaseUrl + "/stationDetails";
        public const string StationDemandes = BaseUrl + "/stationDemandes/";
        public const string NewDemande = BaseUrl + "/addDemande";
        public const string GetPriceTypes = BaseUrl + "/getPriceTypes";
        public const string FilterStationDemandes = BaseUrl + "/filterStationDemandes/";



    }
}
