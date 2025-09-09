using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain;

namespace Auth.API.Models
{
    [Table(name: "AspNetUserMenus", Schema = "sec")]
    public class UserMenu : EntityBase
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuText { get; set; }
        public string? ParentUrl { get; set; }
        public string? ParentComponent { get; set; }
        public string? ChildUrl { get; set; }
        public string? ChildComponent { get; set; }
        public string? IconCss { get; set; }
        public int? ParentId { get; set; }
        public int MenuPosition { get; set; }
        public int DisplayOrder { get; set; }
        public int ModuleId { get; set; }
        public bool IsView { get; set; }
    }
}
