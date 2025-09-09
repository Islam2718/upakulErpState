using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global.Domain.ViewModels;

namespace Global.Domain.Models.Views
{
    [Table("vw_GeoLocation",Schema ="dbo")]
    public class VWGeoLocation
    {
        public int GeoLocationId { get; set; }
        public int GeoLocationType { get; set; }
        public string GeoLocationCode { get; set; }
        public string GeoLocationName { get; set; }
        public int? ParentId { get; set; }

        public int? GeoLocationDivisionId { get; set; }
        public string ?GeoLocationDivisionCode { get; set; }
        public string ?GeoLocationDivisionName { get; set; }
        public int? GeoLocationDistrictId { get; set; }
        public string? GeoLocationDistrictCode { get; set; }
        public string? GeoLocationDistrictName { get; set; }
        public int? GeoLocationThanaId { get; set; }
        public string? GeoLocationThanaCode { get; set; }
        public string? GeoLocationThanaName { get; set; }
        public int? GeoLocationUnionId { get; set; }
        public string? GeoLocationUnionCode { get; set; }
        public string? GeoLocationUnionName { get; set; }
        public int? GeoLocationVillageId { get; set; }
        public string? GeoLocationVillageCode { get; set; }
        public string? GeoLocationVillageName { get; set; }
    }
}
