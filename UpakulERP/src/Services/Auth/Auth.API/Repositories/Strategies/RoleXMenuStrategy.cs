using System.Net;
using System.Threading.Tasks;
using Auth.API.Context;
using Auth.API.DTO.Request;
using Auth.API.Models;
using Auth.API.Repositories.Interfaces;
using Utility.Constants;
using Utility.Response;

namespace Auth.API.Repositories.Strategies
{
    public class RoleXMenuStrategy(AppDbContext context) : IRoleXMenuStrategy
    {
        public async Task<CommadResponse> Create(List<MenuPermissionRequestCommand> request, int logUserid)
        {
            try
            {
                var roleId = request.First().RoleId;
                var lst = context.roleXmenus.Where(x => x.RoleId == roleId);
                if (lst.Any())
                    lst.ToList().ForEach(x => { x.IsActive = false; x.DeletedBy = logUserid; x.DeletedOn = DateTime.Now; });
                foreach (var item in request)
                {
                    if (lst.Where(x => x.MenuId == item.MenuId).Any())
                        lst.Where(x => x.MenuId == item.MenuId).ToList().ForEach(x =>
                        {
                            x.IsActive = true;
                            x.IsDelete = item.IsDelete;
                            x.IsView = item.IsView;
                            x.IsAdd = item.IsAdd;
                            x.IsEdit = item.IsEdit;
                            //x.CreatedBy = logUserid;
                            //x.CreatedOn = DateTime.Now;
                            x.DeletedBy = null;
                            x.DeletedOn = null;
                        });
                    else
                        await context.roleXmenus.AddAsync(new RoleXMenu
                        {
                            CreatedBy = logUserid,
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                            IsAdd = item.IsAdd,
                            IsEdit = item.IsEdit,
                            IsDelete = item.IsDelete,
                            IsView = item.IsView,
                            MenuId = item.MenuId,
                            RoleId = item.RoleId,
                        });
                }
                await context.SaveChangesAsync();
                return new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return new CommadResponse(ex.Message, HttpStatusCode.InternalServerError);
            }

        }
    }
}
