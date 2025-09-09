using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace Accounts.Domain.Models
{
    [Table("Component", Schema = "budg")]
    public class BudgetComponent : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string ComponentName { get; set; }
        public string? LabelShow { get; set; }
        public bool? IsMedical { get; set; } = false;
        public bool? IsDesignation { get; set; } = false;

    }
}
