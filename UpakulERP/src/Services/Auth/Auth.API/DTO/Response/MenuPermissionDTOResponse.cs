namespace Auth.API.DTO.Response
{
    public class MenuPermissionDTOResponse
    {
        public MenusVM parent {  get; set; }
        public List<MenusVM> child {  get; set; }
    }

    public class MenusVM
    {
        public int MenuId { get; set; }
        public string? MenuText { get; set; }
        public string? URL { get; set; } = string.Empty;  // Avoid NULL exceptions
        public string? IconCSS { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int MenuPosition { get; set; }
        public int DisplayOrder { get; set; }
        public bool PermissionType { get; set; }
        public bool IsPermitted { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }
    }
}
