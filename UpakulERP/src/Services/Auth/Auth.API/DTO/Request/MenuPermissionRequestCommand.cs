namespace Auth.API.DTO.Request
{
    public class MenuPermissionRequestCommand
    {
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }
    }
}
