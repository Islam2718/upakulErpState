using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace MF.Domain.Models
{

    [Table("Occupation", Schema = "mem")]
    public class Occupation : EntityBase
    {
        [Key]
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }

    }
}
