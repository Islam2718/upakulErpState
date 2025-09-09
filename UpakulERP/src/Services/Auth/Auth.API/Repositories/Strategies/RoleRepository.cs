using System.Data;
using System.Linq.Expressions;
using System.Net;
using Auth.API.Context;
using Auth.API.DTO.Request;
using Auth.API.Models;
using Auth.API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;

namespace Auth.API.Repositories.Strategies
{
    public class RoleRepository(RoleManager<ApplicationRole> roleManager, AppDbContext context, IMapper mapper) : IRoleRepository
    {
        public async Task<CommadResponse> CreateRoleAsync(CreateRoleDtoRequest request)
        {
            if (RoleNameAlreadyUsedAsync(request.Name, request.ModuleId))
                return new CommadResponse($"Role: {MessageTexts.duplicate_entry}", HttpStatusCode.BadRequest);

            var role = new ApplicationRole
            {
                Name = request.Name,
                ModuleId = request.ModuleId,
                NormalizedName = request.Name.ToUpperInvariant(),
                //ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await roleManager.CreateAsync(role);

            return new CommadResponse((result.Succeeded ? MessageTexts.insert_success : MessageTexts.insert_failed), result.Succeeded ? HttpStatusCode.Created : HttpStatusCode.NotAcceptable);

            //var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Unknown error occurred.";
            //return new CommadResponse(errorMessage, HttpStatusCode.ExpectationFailed);
        }

        public async Task<List<ApplicationRole>> LoadGrid(int moduleId)
        {
            var result = await roleManager.Roles.Where(x=>x.ModuleId==moduleId).ToListAsync();
            return result;
        }

        public async Task<ApplicationRole> GetByRoleId(int roleId)
        {
            var result = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            return result;
        }


        public async Task<CommadResponse> UpdateRoleAsync(UpdateRoleDtoRequest request)
        {
            if (RoleNameAlreadyUsedAsync(request.Name, request.ModuleId,request.Id))
                return new CommadResponse($"Role: {MessageTexts.duplicate_entry}", HttpStatusCode.BadRequest);

            var obj= GetById(request.Id);

            // Update properties
            obj.ModuleId = request.ModuleId;
            obj.Name = request.Name;
            obj.NormalizedName = request.Name.ToUpperInvariant();
            //obj.ConcurrencyStamp = Guid.NewGuid().ToString(); // Updated for concurrency tracking

            var result = await roleManager.UpdateAsync(obj);

            return new CommadResponse((result.Succeeded ? MessageTexts.update_success : MessageTexts.update_failed), result.Succeeded ? HttpStatusCode.Accepted : HttpStatusCode.NotAcceptable);
        }


        public List<CustomSelectListItem> GetRoleByModuleIdDropdown(int moduleId)
        {
            var lst = new List<CustomSelectListItem>();
            lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            lst.AddRange(roleManager.Roles.Where(x => x.ModuleId == moduleId).Select(x =>
            new CustomSelectListItem
            {
                Text = x.Name + " (" + x.Name + ")",
                Value = x.Id.ToString()
            }).OrderBy(x => x.Text).ToList());
            return lst;
        }



        public async Task<CommadResponse> DeleteRoleAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);         
            if (role == null)
            {                
                return new CommadResponse("Role not found.", HttpStatusCode.NotFound);
            }
            else
            {
                bool chkMenu = GetByIdRoleXMenu(int.Parse(roleId));
                bool chkModule = GetByIdroleXModules(int.Parse(roleId));

                if(!chkMenu && !chkModule)
                {
                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        return new CommadResponse("Role deleted successfully.", HttpStatusCode.OK);

                    var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Error deleting role.";
                    return new CommadResponse(errorMessage, HttpStatusCode.ExpectationFailed);

                }

                return new CommadResponse("Failed Delete", HttpStatusCode.ExpectationFailed);
            }

           
        }


        private bool RoleNameAlreadyUsedAsync(string name, int moduleId) =>
                context.Roles.Where(x => x.Name == name && x.ModuleId == moduleId).Any();
        private bool RoleNameAlreadyUsedAsync(string name, int moduleId, int id) =>
                context.Roles.Where(x => x.Name == name && x.ModuleId == moduleId && x.Id != id).Any();
        private ApplicationRole GetById(int rid) =>
                context.Roles.Find(rid);

        private bool GetByIdRoleXMenu(int rid) =>
                context.roleXmenus.Where(x=> x.RoleId==rid).Any();

        private bool GetByIdroleXModules(int rid) =>
                context.roleXModules.Where(x => x.RoleId == rid).Any();
        
    }
}
