using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Utility.Domain;

namespace Project.Domain.Models
{
    [Table("Doner", Schema = "dbo")]
    public class Doner : EntityBase
    {
        [Key]
        public int DonerId { get; set; }
        public string? DonerCode { get; set; }
        public string DonerName { get; set; }
        public int CountryId { get; set; }
        public string? FirstContactPersonName { get; set; }
        public string? FirstContactPersonContactNo { get; set; }
        public string? SecendContactPersonName { get; set; }
        public string? SecendContactPersonContactNo { get; set; }
        public string? Website { get; set; }
        public string? Location { get; set; }
    }
}
