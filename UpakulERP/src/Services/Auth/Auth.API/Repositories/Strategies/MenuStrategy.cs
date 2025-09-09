using System.Net;
using Auth.API.Context;
using Auth.API.DTO;
using Auth.API.DTO.Response;
using Auth.API.Models;
using Auth.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;

namespace Auth.API.Repositories.Strategies
{
    public class MenuStrategy(AppDbContext context) : IMenuStrategy
    {
        // previous
        public List<ModuleXMenus> GetMenuBy(int moduleId, int roleId)
        {
            try
            {
                var lst = (from m in context.userMenus
                           join rm in context.roleXmenus on m.MenuId equals rm.MenuId
                           where m.IsActive && rm.IsActive && rm.RoleId == roleId && m.ModuleId == moduleId
                           orderby m.ModuleId, (m.ParentId ?? 0), m.DisplayOrder, m.MenuPosition
                           select new
                           {
                               menu_id = m.MenuId,
                               icon_css = m.IconCss,
                               menu_position = m.MenuPosition,
                               display_order = m.DisplayOrder,
                               menu_text = m.MenuText,
                               parent_id = m.ParentId,
                               url_text = m.ParentUrl,
                               Component = m.ParentComponent,
                               ChildUrl = "",
                               ChildComponent = "",
                           }).ToList();
                #region Json Generat
                var parentMenu = lst.Where(x => (x.parent_id ?? 0) == 0).OrderBy(x => x.display_order);
                List<ModuleXMenus> roleXMenuViewModels = new List<ModuleXMenus>();
                foreach (var p in parentMenu)
                {
                    ModuleXMenus roleXMenuViewModel = new ModuleXMenus();
                    var menu = new Menu
                    {
                        display_order = p.display_order,
                        icon_css = p.icon_css,
                        menu_position = p.menu_position,
                        menu_text = p.menu_text,
                        url_text = p.url_text,
                    };
                    roleXMenuViewModel.menu = menu;
                    if (lst.Where(x => x.parent_id == p.menu_id).Any())
                    {
                        var child_menu = lst.Where(x => x.parent_id == p.menu_id);
                        List<HigherArcheryMenu> higher_menus = new List<HigherArcheryMenu>();
                        foreach (var c in child_menu)
                        {
                            HigherArcheryMenu higherArcheryMenu = new HigherArcheryMenu();
                            var menu_inner = new Menu
                            {
                                display_order = c.display_order,
                                icon_css = c.icon_css,
                                menu_position = c.menu_position,
                                menu_text = c.menu_text,
                                url_text = c.url_text,
                            };
                            higherArcheryMenu.menu = menu_inner;

                            var sub_child_menu = lst.Where(x => x.parent_id == c.menu_id);
                            List<Menu> sub_child_menus = new List<Menu>();
                            foreach (var s in sub_child_menu)
                            {
                                var _inner = new Menu
                                {
                                    display_order = c.display_order,
                                    icon_css = c.icon_css,
                                    menu_position = c.menu_position,
                                    menu_text = c.menu_text,
                                    url_text = c.url_text,
                                };
                                sub_child_menus.Add(_inner);
                            }
                            higherArcheryMenu.childs = sub_child_menus;
                            higher_menus.Add(higherArcheryMenu);
                        }
                        roleXMenuViewModel.child = higher_menus;
                        roleXMenuViewModels.Add(roleXMenuViewModel);
                    }
                }
                #endregion json generat


                return roleXMenuViewModels;
            }
            catch (Exception ex)
            {
                return new List<ModuleXMenus>();

            }
        }

        public List<UserMenuVM> GetMenuListbyModule(int moduleId, int roleId)
        {
             
            var lst = (from m in context.userMenus
                       join rm in context.roleXmenus on m.MenuId equals rm.MenuId
                       where m.IsActive && rm.IsActive && rm.RoleId == roleId && m.ModuleId == moduleId
                       orderby m.ModuleId, (m.ParentId ?? 0), m.DisplayOrder, m.MenuPosition
                       select new UserMenuVM
                       {
                           MenuId = m.MenuId,
                           MenuText = m.MenuText,
                           // Component Routing
                           URL = m.ParentUrl,
                           Component = m.ParentComponent,
                           ChildUrl = m.ChildUrl,
                           ChildComponent = m.ChildComponent,
                          // End
                           IconCss = m.IconCss,
                           ParentId = m.ParentId,
                           MenuPosition = m.MenuPosition,
                           DisplayOrder = m.DisplayOrder,
                           IsViewMenu = m.IsView,
                           IsAdd = rm.IsAdd,
                           IsEdit = rm.IsEdit,
                           IsDelete = rm.IsDelete,
                           IsView = rm.IsView,


                       }).OrderBy(x=>x.DisplayOrder).ThenBy(x=>x.MenuPosition).Distinct().ToList();
            if (lst.Any())
                return lst;
            else return new List<UserMenuVM>();
        }
        //public List<CustomSelectListItem> GetMenubyModule(int moduleId)
        //{
        //    var lst = new List<CustomSelectListItem>();
        //    lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
        //    lst.AddRange(
        //     context.userMenus.Where(x => x.IsActive && x.ModuleId == moduleId).Select(x => new CustomSelectListItem
        //     {
        //         Text = x.MenuText,
        //         Value = x.MenuId.ToString(),
        //         //Url = x.URL
        //     }));
        //    return lst;
        //}
        
        public List<CustomSelectListItem> GetMenubyModule(int moduleId)
        {
            var lst = new List<CustomSelectListItem>();
            lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            lst.AddRange(
             context.MenuHierarchicals.FromSql($"SELECT * FROM [sec].[udf_MenuHierarchical]({moduleId})").ToList().Select(x => new CustomSelectListItem
             {
                 Text = x.MenuText,
                 Value = x.MenuId.ToString(),
                 //Url = x.URL
             }));
            return lst;
        }

        public List<MenuPermissionDTOResponse> GetMenuPermission(int moduleId, int roleId)
        {
            var lst = context.Database.SqlQuery<MenusVM>($"[sec].[uspMenuPermission] {roleId},{moduleId}").ToList();
            if (lst.Any())
            {
                List<MenuPermissionDTOResponse> menuPermissionDTOResponses = new List<MenuPermissionDTOResponse>();
                MenuPermissionDTOResponse obj = new MenuPermissionDTOResponse();
                obj.child = lst;
                // Mahfuz 2025 05 05
                // First position 
                //  var first_pos = lst.Where(x => (x.ParentId ?? 0) == 0);
                /// var first_pos = lst.Where(x => (x.ParentId ?? 0) == 0);
                //foreach (var menu in lst)
                //{
                //    var menu_lst = GetChildMenu(menu.MenuId, lst);
                //    menuPermissionDTOResponses.Add(new MenuPermissionDTOResponse { parent = menu, child = menu_lst });
                //}
                menuPermissionDTOResponses.Add(obj);
                return menuPermissionDTOResponses;
            }

            return new List<MenuPermissionDTOResponse>();
        }

        public CommadResponse CreateMenu(UserMenu menu)
        {
            if ((menu.ParentId ?? 0) == 0)
            {
                menu.MenuPosition = 1;
                menu.DisplayOrder = context.userMenus.Where(x => x.IsActive && (x.ParentId ?? 0) == 0).Max(x => x.DisplayOrder);
            }
            else
            {
                menu.MenuPosition = context.userMenus.FirstOrDefault(x => x.IsActive && x.MenuId == (menu.ParentId ?? 0)).MenuPosition + 1;
                menu.DisplayOrder = context.userMenus.Where(x => x.IsActive && x.ParentId == (menu.ParentId ?? 0)).Count() + 1;
            }
            context.userMenus.Add(menu);
            var status = context.SaveChanges();
            return (status == 1 ? new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created) : new CommadResponse(MessageTexts.insert_failed, HttpStatusCode.BadRequest));
        }

        private List<MenusVM> GetChildMenu(int pid, List<MenusVM> lst)
        {
            List<MenusVM> menuList = new List<MenusVM>();
            var p_lst = lst.Where(x => x.ParentId == pid);

            foreach (var menu in p_lst)
            {
                menuList.Add(menu);
                var child_lst = GetChildMenu(menu.MenuId, lst);
                if (child_lst.Any())
                    menuList.AddRange(child_lst);
            }
            return menuList;
        }
    }
}
