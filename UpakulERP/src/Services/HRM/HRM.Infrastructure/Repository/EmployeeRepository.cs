using Dapper;
using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using UpakulHRM.Infrastructure.Persistence;
using Utility.Domain;
using Utility.Response;
using System.Linq.Dynamic.Core;
using HRM.Domain.Models.Views;

namespace HRM.Infrastructure.Repository
{
    public class EmployeeRepository : CommonRepository<Employee>, IEmployeeRepository
    {
        AppDbContext _context;
        private readonly string _connectionString;
        public EmployeeRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Employee> GetById(int id)
        {
            var obj = await _context.employees.FirstOrDefaultAsync(c => c.IsActive && c.EmployeeId == id);
            return obj;
        }

        public async Task<VWEmployee> GetById_View(int id)
        {
            var obj = await _context.vwEmployees.FirstOrDefaultAsync(c => c.EmployeeId == id);
            return obj;
        }
        public IEnumerable<Employee> GetMany(Expression<Func<Employee, bool>> where)
        {
            var entities = _context.employees.Where(where).Where(b => b.IsActive);
            return entities;
        }
     
        public async Task<MultipleDropdownForEmployeeProfileVM> AllEmployeeProfilesDropDown(int officeId)
        {
            try
            {
                var obj = new MultipleDropdownForEmployeeProfileVM();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var param = new { officeId = officeId };
                    using (var multi = await connection.QueryMultipleAsync("udp_AllEmployeeProfilesDropDown", param, commandType: CommandType.StoredProcedure))
                    {
                        obj.department = multi.Read<CustomSelectListItem>().ToList();
                        obj.designation = multi.Read<CustomSelectListItem>().ToList();
                        obj.country = multi.Read<CustomSelectListItem>().ToList();
                        obj.office = multi.Read<CustomSelectListItem>().ToList();
                        obj.circular = multi.Read<CustomSelectListItem>().ToList();
                        obj.bank = multi.Read<CustomSelectListItem>().ToList();
                        obj.division = multi.Read<CustomSelectListItem>().ToList();
                        obj.occupation = multi.Read<CustomSelectListItem>().ToList();
                    }
                }
                obj.employeeType = new Utility.Enums.HRM.EmployeeType().GetEmployeeTypeDropDown();
                obj.employeeStatus = new Utility.Enums.HRM.EmployeeStatus().GetEmployeeStatusDropDown();
                obj.gender = new Utility.Enums.Gender().GetGenderDropDown();
                obj.religion = new Utility.Enums.Religion().GetReligionDropDown();
                obj.bloodGroup = new Utility.Enums.BloodGroup().GetBloodGroupDropDown();
                obj.maritalStatus = new Utility.Enums.MaritalStatus().GetMaritalStatusDropDown(true);
                return obj;
            }
            catch (Exception ex)
            {
                return new MultipleDropdownForEmployeeProfileVM();
            }
        }

        public async Task<PaginatedResponse<EmployeeVM>> LoadGrid(int page, int pageSize, string search, string sortOrder,int officeId)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "EmployeeCode.Contains(@0) OR FirstName.Contains(@0) OR LastName.Contains(@0) OR DepartmentName.Contains(@0) OR DesignationName.Contains(@0) OR MaritalStatus.Contains(@0) OR OfficeCode.Contains(@0) OR OfficeName.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "DesignationId" : sortOrder;

            string qry = @$"SELECT OfficeId FROM dbo.udf_OfficeHierarchical({officeId},0)";
            var officelst = await _context.Database.SqlQueryRaw<int>(qry).ToArrayAsync();

            var query = _context.vwEmployees.Where(x=> officelst.Contains(x.OfficeId))
                .Select(x => new EmployeeVM
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeCode = x.EmployeeCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmployeeNameBn = x.EmployeeNameBn,
                    EmployeePicURL = x.EmployeePicURL,
                    DepartmentName = x.DepartmentName,
                    DesignationName = x.DesignationName,
                    MaritalStatus = x.MaritalStatus,
                    OfficeCode = x.OfficeCode,
                    OfficeName= x.OfficeName,
                }).Where(src_qry, search).OrderBy(sortOrder).AsQueryable();


            // Pagination
            var totalRecords = await query.CountAsync();
            var lst = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<EmployeeVM>(lst, totalRecords);
        }
    }
}
