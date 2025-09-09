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
    [Table("GroupCommittee", Schema = "mem")]
    public class GroupCommittee 
    {
      [Key]
      public int Id { get; set; }
      public int GroupId { get; set; }
      public int CommitteePositionId { get; set; }
      public int MemberId { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime? EndDate { get; set; }
      public bool IsActive { get; set; } = true;
      public int? CreatedBy { get; set; }
      public DateTime? CreatedOn { get; set; }
      public int? DeletedBy { get; set; }
      public DateTime? DeletedOn { get; set; }     
    }
}
