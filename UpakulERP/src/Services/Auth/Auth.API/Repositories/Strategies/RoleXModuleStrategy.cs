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
    public class RoleXModuleStrategy(AppDbContext context) : IRoleXModuleStrategy
    {
        public async Task<CommadResponse> Create(List<RoleXModule> request)
        {
            try
            {
                var roleId = request.First().RoleId;
                var uId = request.First().UserId;
                var logUserid = request.First().CreatedBy;
                var lst = context.roleXModules.Where(x => /*x.RoleId == roleId &&*/ x.UserId == uId);
                if (lst.Any())
                    lst.ToList().ForEach(x => { x.IsActive = false; x.DeletedBy = logUserid; x.DeletedOn = DateTime.Now; });
                foreach (var item in request)
                {
                    if (lst.Where(x => x.ModuleId == item.ModuleId).Any())
                        lst.Where(x => x.ModuleId == item.ModuleId).ToList().ForEach(x =>
                        {
                            x.IsActive = true;
                            x.CreatedBy = logUserid;
                            x.CreatedOn = DateTime.Now;
                        });
                    else
                        await context.roleXModules.AddAsync(new RoleXModule
                        {
                            CreatedBy = logUserid,
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                            ModuleId = item.ModuleId,
                            RoleId = item.RoleId,
                            UserId = item.UserId,
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
