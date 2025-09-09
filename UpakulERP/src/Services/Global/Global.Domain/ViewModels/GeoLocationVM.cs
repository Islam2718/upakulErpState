namespace Global.Domain.ViewModels
{
    public class GeoLocationVM
    {
        public int GeoLocationId { get; set; }
        public string GeoLocationCode { get; set; }
        public string GeoLocationName { get; set; }
        public int GeoLocationType { get; set; }
        public int? ParentId { get; set; }

    }
}
