using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace Global.Domain.Models
{
    [Table("GeoLocation", Schema ="dbo")]
    public class GeoLocation: EntityBase
    {
        [Key]
        public int GeoLocationId { get; set; }
        public string GeoLocationCode { get; set; }
        public string GeoLocationName { get; set; }
        public int GeoLocationType { get; set; }
        public int? ParentId { get; set; }
    }
}
