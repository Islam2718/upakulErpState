using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace Utility.Domain.DBDomain

{
    [Table("Country", Schema = "dbo")]
    public class CommonCountry
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
