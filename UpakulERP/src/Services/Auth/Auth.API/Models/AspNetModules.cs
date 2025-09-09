using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Models
{
    [Table(name: "AspNetModules",Schema ="sec")]
    public class AspNetModules
    {
        [Key]
        public int ModuleId { get; set; }
        public string SecurityKey { get; set; }
        public string ModuleName { get;set; }
        public string ModuleSecendDivClass { get;set; }
        public string ModuleIconClass { get;set; }
        public string ModuleTitle {  get;set; }
        public string ModuleURL { get;set; }
        public int DisplayOrder { get;set; }
        public bool IsActive { get;set; }
    }
}
