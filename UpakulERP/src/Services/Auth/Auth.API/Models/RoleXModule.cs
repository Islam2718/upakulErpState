using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Utility.Domain;

namespace Auth.API.Models
{
    [Table(name: "AspNetRolesXModule", Schema = "sec")]
    //[PrimaryKey(nameof(RoleId), nameof(ModuleId), nameof(IsActive))]
    //[Keyless]
    public class RoleXModule
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        //[Column(Order = 0)]
        public int RoleId { get; set; }
        //[Column(Order = 1)]
        public int ModuleId { get; set; }
        //[Column(Order = 2)]
        public bool IsActive { get; set; } = true;
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? DeletedBy {  get; set; }
        public DateTime? DeletedOn { get;set; }
    }
}
