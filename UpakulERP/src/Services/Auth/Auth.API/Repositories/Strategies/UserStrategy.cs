using System.Net;
using Auth.API.Context;
using Auth.API.DTO.Request;
using Auth.API.DTO.Response;
using Auth.API.Models;
using Auth.API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility.Constants;
using Utility.Response;
using Microsoft.Data.SqlClient;
using System.Data;
using Message.Infrastructure.Repository.Interfaces;
using Message.Domain.Models;
using Message.Library.Template.User;
using Message.Library.Contacts.Repository;
using Message.Library.Model;

namespace Auth.API.Repositories.Strategies
{
    public class UserStrategy(SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager, AppDbContext context, IMapper mapper, IUserMailLogRepository _userMailLogRepository) : IUserStrategy
    {
        public async Task<CommadResponse> CreateUserAsync(UserDtoRequest request)
        {

            if (await IsUIDAlreadyUsedAsync(request.UserName!))
                return new CommadResponse(MessageTexts.duplicate_entry("User:"), HttpStatusCode.BadRequest);
            else if (await IsEmployeeAlreadyUsedAsync(request.EmployeeId))
                return new CommadResponse("Same employee multiple user not allow.", HttpStatusCode.BadRequest);
            else
            {
                var user = new ApplicationUser()
                {
                    Email = request.Email,
                    EmployeeId = request.EmployeeId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.UserName,
                    CreatedBy = request.LoginUser,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                };
                var result = await userManager.CreateAsync(user, request.Password);
                var getUser = await UserAsync(request.UserName);

                if (result.Succeeded) return new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Created, ReturnId: getUser.Id);
                else return new CommadResponse(result.Errors.First().Description, HttpStatusCode.ExpectationFailed);
            }
        }
        public async Task<CommadResponse> ResetPasswordAsync(ResetPasswordDtoRequest request, string loginUser)
        {
            var user = await UserAsync(request.UserName);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);

                if (result.Succeeded)
                {
                    try
                    {
                        var tmp = new ResetPasswordTemplate().ResetPassword(user.UserName, request.NewPassword, loginUser);
                        var mail = new EmailMessage()
                        {
                            To = user.Email,
                            ToDisplayName = user.FirstName,
                            Body = tmp.Item2,
                            Subject = tmp.Item1,
                        };
                        if (await new EmailService().SendEmailAsync(mail))
                        {
                            UserMailLog obj = new UserMailLog()
                            {
                                IsSend = true,
                                MailBody = mail.Body,
                                MailTo = mail.To,
                                Purpus = "System reset pasword",
                                SenderUser = loginUser,
                                UserId = user.UserName
                            };
                            _userMailLogRepository.Create(obj);
                        }
                    }
                    catch (Exception ex) { }
                    
                    return new CommadResponse((MessageTexts.reset_password_success+$" new password is {request.NewPassword}"), HttpStatusCode.Accepted);
                }
                else return new CommadResponse(result.Errors.First().Description, HttpStatusCode.ExpectationFailed);
            }
            else return new CommadResponse("User Not found", HttpStatusCode.NotFound);
        }

        public async Task<CommadResponse> ChangePasswordAsync(string UserName, ChangePasswordDtoRequest request)
        {
            var user = await UserAsync(UserName);
            if (user != null)
            {
                var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (result.Succeeded) return new CommadResponse(MessageTexts.insert_success, HttpStatusCode.Accepted);
                else return new CommadResponse(result.Errors.First().Description, HttpStatusCode.ExpectationFailed);
            }
            else return new CommadResponse("User Not found", HttpStatusCode.NotFound);
        }

        public async Task<LoginDtoResponse> SignIn(LoginDtoRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserId);
            if (user == null)
            {
                LoginDtoResponse res = new(Message: MessageTexts.user_not_found);
                return res;
            }
            else if (!user.IsActive)
            {
                LoginDtoResponse res = new(Message: MessageTexts.user_not_found);
                return res;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                var mapped = mapper.Map<LoginDtoResponse>(user);
                return mapped;
            }
            else { LoginDtoResponse res = new(Message: "Wrong password."); return res; }
        }

        public async Task<ApplicationUser> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.IsActive && x.Id == id);
        }



        public async Task<CommadResponse> DeleteUserAsync(UserDeleteDtoRequest request)
        {
            var user = context.Users.First(a => a.Id == request.UserId);

            if (user == null)
            {
                return new CommadResponse("User not found.", HttpStatusCode.NotFound);
            }
            else
            {

                user.IsActive = false;
                user.DeletedBy = request.LoginUser;
                user.DeletedOn = DateTime.Now;
                user.IsActive = false;
                context.SaveChanges();
                return new CommadResponse("User deleted successfully.", HttpStatusCode.OK);
            }
        }

        public async Task<PaginatedResponse<UsersGridResponse>> LoadGrid(int page, int pageSize, string search, string sortOrder, int officeid)
        {

            var prmLst = new List<object>();
            prmLst.Add(new SqlParameter("@pageNumber", SqlDbType.Int) { Value = page });
            prmLst.Add(new SqlParameter("@rowsOfPage", SqlDbType.Int) { Value = pageSize });
            prmLst.Add(new SqlParameter("@searching", SqlDbType.VarChar) { Value = search });
            prmLst.Add(new SqlParameter("@sortOrder", SqlDbType.VarChar) { Value = sortOrder });
            prmLst.Add(new SqlParameter("@officeId", SqlDbType.Int) { Value = officeid });
            string sql = $"EXEC [sec].[udp_UserGrid] @pageNumber,@rowsOfPage,@searching,@sortOrder,@officeId";
            var lst = await context.Database.SqlQueryRaw<UsersGrid>(sql, prmLst.ToArray()).ToListAsync();

            int totalRecords = 0;
            var data = new List<UsersGridResponse>();
            if (lst.Any())
            {
                totalRecords = lst.First().TotalCount ?? 0;
                data = lst.Select(x => new UsersGridResponse
                {
                    Email = x.Email,
                    EmployeeId = x.EmployeeId,
                    EmployeeCode = x.EmployeeCode,
                    FullName = x.FullName,
                    Id = x.Id,
                    OfficeName = x.OfficeName,
                    UserName = x.UserName,
                }).ToList();
            }

            //search = search ?? "0";
            //string src_Qry = string.IsNullOrEmpty(search) ? "@0=@0" : "UserName.Contains(@0) OR Email.Contains(@0) OR EmployeeCode.Contains(@0) OR FullName.Contains(@0) OR OfficeName.Contains(@0)";
            //sortOrder = string.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;
            //var officeHierarchy = context.GetOfficeHierarchi(officeid, 0).AsEnumerable();
            //var query = (from user in context.Users
            //             join emp in context.employees on user.EmployeeId equals emp.EmployeeId
            //             join o in officeHierarchy on emp.OfficeId equals o.OfficeId
            //             where user.IsActive
            //             select new UsersGridResponse
            //             {
            //                 Id = user.Id,
            //                 UserName = user.UserName ?? "",
            //                 Email = user.Email,
            //                 EmployeeCode = emp.EmployeeCode,
            //                 FullName = emp.FirstName + " " + emp.LastName ?? "",
            //                 OfficeName = o.OfficeCode + " - " + o.OfficeName
            //             }).AsQueryable().Where(src_Qry, search).OrderBy(sortOrder).AsQueryable();

            //// Pagination
            //var totalRecords = await query.CountAsync();
            //var lst = await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginatedResponse<UsersGridResponse>(data, totalRecords);
        }

        private async Task<bool> IsUIDAlreadyUsedAsync(string uid) =>
                await userManager.FindByNameAsync(uid) != null;
        //private async Task<IEnumerable<ApplicationUser>> IsExistinMobileOrEmail(string? email, string? mobileno) =>
        //    await context.Users.Where(x => (x.Email == (email ?? "") || x.PhoneNumber == (mobileno ?? "")) && !string.IsNullOrEmpty(x.PhoneNumber));
        private async Task<ApplicationUser> UserAsync(string uid) =>
               await userManager.FindByNameAsync(uid);

        private async Task<bool> IsEmployeeAlreadyUsedAsync(int eid) =>
               await context.Users.FirstOrDefaultAsync(x => x.EmployeeId == eid) != null;
    }
}
