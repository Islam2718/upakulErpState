using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Domain.Models
{
    [Table("IdGenerate", Schema = "conf")]
    public  class IdGenerate
    {
        [Key]
        public int Id { get; set; }
        public bool? IsReset { get; set; } = true;
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int CodeLength { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public string MainJoinCode { get; set; }
        public string? VirtualJoinCode { get; set; }
    }
}

