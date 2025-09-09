namespace Auth.API.DTO.Response
{
    public class UserMenuVM
    {
        public int MenuId { get; set; }
        public string MenuText { get; set; }
        public string? URL { get; set; }
        public string? Component { get; set; }
        public string? ChildUrl { get; set; }
        public string? ChildComponent { get; set; }
        public string? IconCss { get; set; }
        public int? ParentId { get; set; }
        public int MenuPosition { get; set; }
        public int DisplayOrder { get; set; }
        //public int ModuleId { get; set; }
        public bool IsViewMenu { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }
    }
}
