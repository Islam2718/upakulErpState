using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using UpakulHRM.Infrastructure.Persistence;
using Utility.Response;

namespace HRM.Infrastructure.Repository
{

    public class DepartmentRepository : CommonRepository<Department>, IDepartmentRepository
    {
        AppDbContext _context;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Department GetById(int id)
        {
            var obj = _context.departments.FirstOrDefault(c => c.IsActive && c.DepartmentId == id);
            return obj;
        }

        public List<Department> GetAll()
        {
            var objlst = _context.departments.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Department> GetMany(Expression<Func<Department, bool>> where)
        {
            var entities = _context.departments.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<DepartmentVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "DepartmentCode.Contains(@0) OR DepartmentName.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "DepartmentId" : sortOrder;
            var query = _context.departments.Where(b => b.IsActive)
                 .Select(x => new DepartmentVM
                 {
                     DepartmentCode=x.DepartmentCode,
                     DepartmentName=x.DepartmentName,
                     DepartmentId=x.DepartmentId,
                     OrderNo=x.OrderNo
                 }).AsQueryable().Where(src_qry, search).OrderBy(sortOrder);

            // Pagination
            var totalRecords = await query.CountAsync();
            var departments = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<DepartmentVM>(departments, totalRecords);
        }

        public async Task<IEnumerable<Department>> GetDeartment()
        {
            var departmentlst = await _context.departments.Where(c => c.IsActive).ToListAsync();
            return departmentlst;
        }
    }
}
