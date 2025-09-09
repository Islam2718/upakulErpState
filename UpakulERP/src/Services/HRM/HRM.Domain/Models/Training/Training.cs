using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace HRM.Domain.Models.Training
{
    [Table("Training", Schema = "emp")]
    public class Training : EntityBase
    {
        [Key]
        public int TrainingId { get; set; }
        public string Title { get; set; }
        public string Institute { get; set; }
        public string? reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
