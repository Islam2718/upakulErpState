using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utility.Domain.DBDomain
{
    [Table("GeoLocation", Schema = "dbo")]
    public class CommonGeoLocation
    {
        [Key]
        public int GeoLocationId { get; set; }
        public int GeoLocationType { get; set; }
        public string    GeoLocationCode { get; set; }
        public string GeoLocationName { get; set; }
        public  int?   ParentId { get; set; }
        public bool IsActive { get; set; }=true;
    }
}
