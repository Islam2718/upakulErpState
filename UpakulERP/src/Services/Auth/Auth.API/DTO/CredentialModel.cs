using Auth.API.DTO.Response;
using Utility.Domain;
using Utility.Domain.DBDomain;

namespace Auth.API.DTO
{
    public class CredentialModel
    {
        public string token { get; set; }
        public string transactionDate {  get; set; }
        public Personal personal { get; set; }
        public List<RoleXModuleModel> modules { get; set; }
      //  public List<ModuleXMenus> menus { get; set; }
        //public List<UserMenu> menus_ { get; set; }
        public List<UserMenuVM> menus { get; set; }
        public Notification notification { get; set; }
    }
    #region Menu
    public class ModuleXMenus
    {
        public Menu menu { get; set; }
        public List<HigherArcheryMenu> child { get; set; }
    }

    public class HigherArcheryMenu
    {
        public Menu menu { get; set; }
        public List<Menu> childs { get; set; }

    }
    public class Menu
    {
        public string menu_text { get; set; }
        public string? url_text { get; set; }
        public string? icon_css { get; set; }
        public int? parent_id { get; set; }
        public int menu_position { get; set; }
        public int display_order { get; set; }

    }
    //public class ModuleXMenus
    //{
    //    public string menu_text { get; set; }
    //    public string? url_text { get; set; }
    //    public string? icon_css { get; set; }
    //    public int? parent_id { get; set; }
    //    public int menu_position { get; set; }
    //    public int display_order { get; set; }
    //}
    #endregion Menu
    public class RoleXModuleModel
    {
        public int module_id { get; set; }
        public int role_id { get; set; }
        public string module_name { get; set; }
        public string secend_div_class { get; set; }
        public string icon_class { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int display_order { get; set; }
    }

    
    
}
