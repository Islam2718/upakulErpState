using System.Data;
using System.Linq.Dynamic.Core;
using System.Net;
using Dapper;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    public class GroupWiseEmployeeAssignRepository : CommonRepository<GroupWiseEmployeeAssign>, IGroupWiseEmployeeAssignRepository
    {
        AppDbContext _context;
        private readonly string _connectionString;

        public GroupWiseEmployeeAssignRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<CustomSelectListItem> GetGroupByEmployeeId(int? employeeId = 0)
        {
            try
            {
                var result = (from ge in _context.groupWiseEmployeeAssigns
                              join g in _context.groups
                              on ge.GroupId equals g.GroupId
                              where ge.IsActive && g.IsActive && ge.EmployeeId == employeeId && ge.ReleaseDate==null
                              select new CustomSelectListItem
                              {
                                  Value = ge.GroupId.ToString(),
                                  Text = g.GroupCode + " - " + g.GroupName
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CustomSelectListItem>();
            }
        }

        public async Task<CommadResponse> CreateOrUpdateAsync(int? AssignEmployeeId, List<int> AssignedGroupListId, int? ReleaseEmployeeId, List<int>? ReleaseGroupListId
            , int? loggedinEmpId, DateTime? releaseDate, string? releaseNote, DateTime? assignDate)
        {
            try
            {
                if ((ReleaseEmployeeId ?? 0) > 0)
                {
                    // Release 
                    var rel_emp_lst = new List<GroupWiseEmployeeAssign>();
                    if (ReleaseGroupListId.Any())
                        rel_emp_lst = _context.groupWiseEmployeeAssigns.Where(x => x.EmployeeId == ReleaseEmployeeId.Value && !ReleaseGroupListId.Contains(x.GroupId) && x.IsActive && x.ReleaseDate == null).ToList();
                    else
                        rel_emp_lst = _context.groupWiseEmployeeAssigns.Where(x => x.EmployeeId == ReleaseEmployeeId.Value && x.IsActive && x.ReleaseDate == null).ToList();
                    if (rel_emp_lst.Any() /*&& ReleaseGroupListId.Any()*/)
                    {
                        rel_emp_lst.ToList().ForEach(x => { x.ReleaseDate = releaseDate; x.ReleaseNote = releaseNote; x.UpdatedOn = DateTime.Now; x.UpdatedBy = loggedinEmpId; });
                        await _context.SaveChangesAsync();
                        //foreach (var gid in ReleaseGroupListId)
                        //{
                        //    if (rel_emp_lst.Where(x => x.GroupId == gid).Any())
                        //        rel_emp_lst.Where(x => x.GroupId == gid).ToList().
                        //            ForEach(x => { x.ReleaseDate = releaseDate; x.ReleaseNote = (releaseNote ?? ""); x.UpdatedBy = loggedinEmpId; x.UpdatedOn = DateTime.Now; });
                        //}

                    }
                }
                // Assign
                if ((AssignEmployeeId ?? 0) > 0 && AssignedGroupListId != null)
                {
                    // Release 
                    var asg_emp_lst = _context.groupWiseEmployeeAssigns.Where(x => x.EmployeeId == AssignEmployeeId.Value && x.IsActive && x.ReleaseDate == null);
                    if (AssignedGroupListId.Any())
                    {
                        foreach (var gid in AssignedGroupListId)
                        {
                            if (!asg_emp_lst.Any(x => x.GroupId == gid))
                            {
                                GroupWiseEmployeeAssign obj = new GroupWiseEmployeeAssign()
                                {
                                    GroupId = gid,
                                    EmployeeId = AssignEmployeeId.Value,
                                    JoiningDate = assignDate.Value,
                                    CreatedBy = loggedinEmpId,
                                    CreatedOn = DateTime.Now,
                                };
                                await _context.groupWiseEmployeeAssigns.AddAsync(obj);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                return new CommadResponse(MessageTexts.update_success, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return new CommadResponse(ex.Message, HttpStatusCode.InternalServerError);
            }


        }

        public async Task<PaginatedResponse<GroupWiseEmployeeAssignVM>> LoadGrid(int page, int pageSize, string search, string sortOrder, int? logedInOfficeId)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "GroupName.Contains(@0) OR GroupCode.Contains(@0) OR EmployeeName.Contains(@0) OR EmployeeCode.Contains(@0)";  // OR JoiningDateString.Contains(@0) OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;

            var query = (from gw in _context.groupWiseEmployeeAssigns
                         join gp in _context.groups on gw.GroupId equals gp.GroupId
                         join em in _context.employees on gw.EmployeeId equals em.EmployeeId
                         where gw.IsActive && gp.OfficeId == logedInOfficeId && gw.ReleaseDate == null
                         select new GroupWiseEmployeeAssignVM
                         {
                             Id = gw.Id,
                             EmployeeId = em.EmployeeId,
                             EmployeeName = em.EmployeeFullName,
                             EmployeeCode = em.EmployeeCode,
                             GroupId = gp.GroupId,
                             GroupName = gp.GroupName,
                             GroupCode = gp.GroupCode,
                             JoiningDate = gw.JoiningDate,
                             JoiningDateString = gw.JoiningDate.ToString("dd-mm-yyyy")
                         })
                .Where(src_qry, search)
                .OrderBy(sortOrder)
                .AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var obj = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<GroupWiseEmployeeAssignVM>(obj, totalRecords);
        }

        public GroupWiseEmployeeAssign ReleaseById(int id)
        {
            var obj = _context.groupWiseEmployeeAssigns.FirstOrDefault(c => c.IsActive && c.Id == id);
            return obj;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<MultipleDropdownForGrpWiseEmployeeVM> AllDropDownForGrpWiseEmployee(int officeId, int officeTypeId)
        {
            try
            {
                var obj = new MultipleDropdownForGrpWiseEmployeeVM();
                //_context.Database.GetDbConnection().ConnectionString
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var param = new { officeId = officeId, officeTypeId = officeTypeId };
                    using (var multi = await connection.QueryMultipleAsync("[mem].udp_GroupXEmployeeDropDown", param, commandType: CommandType.StoredProcedure))
                    {
                        obj.availableGroup = multi.Read<CustomSelectListItem>().ToList();
                        obj.releaseEmployee = multi.Read<CustomSelectListItem>().ToList();
                        obj.assignEmployee = multi.Read<CustomSelectListItem>().ToList();
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                return new MultipleDropdownForGrpWiseEmployeeVM();
            }
        }
    }
}
