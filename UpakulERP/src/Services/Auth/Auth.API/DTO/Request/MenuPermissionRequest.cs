namespace Auth.API.DTO.Request
{
    public class MenuPermissionRequest
    {
        public int MenuId { get; set; }
        public string MenuText { get; set; }
        public string URL { get; set; }
        public string IconCSS { get; set; }
        public int? ParentId { get; set; }

        public int MenuPosition { get; set; }
        public int DisplayOrder { get; set; }
        public bool PermissionType { get; set; }
        
    }
}
