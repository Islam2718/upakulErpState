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
    public class DesignationRepository : CommonRepository<Designation>, IDesignationRepository
    {
        AppDbContext _context;
        public DesignationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Designation GetById(int id)
        {
            var obj = _context.designations.FirstOrDefault(c => c.IsActive && c.DesignationId == id);
            return obj;
        }

        public List<Designation> GetAll()
        {
            var objlst = _context.designations.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Designation> GetMany(Expression<Func<Designation, bool>> where)
        {
            var entities = _context.designations.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<DesignationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "DesignationCode.Contains(@0) OR DesignationName.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "DesignationId" : sortOrder;
            var query = _context.designations.Where(b => b.IsActive)
                .Select(x => new DesignationVM
                {
                    DesignationCode = x.DesignationCode,
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    OrderNo = x.OrderNo
                }).AsQueryable().Where(src_qry, search).OrderBy(sortOrder);


            // Pagination
            var totalRecords = await query.CountAsync();
            var designations = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<DesignationVM>(designations, totalRecords);
        }


        public async Task<IEnumerable<Designation>> GetDesignation()
        {
            var designationlst = await _context.designations.Where(c => c.IsActive).ToListAsync();
            return designationlst;
        }

        //public async Task<Designation> AddDesignation(Designation designation)
        //{
        //    // Add the new designation to the context
        //    await _context.designations.AddAsync(designation);

        //    // Save the changes to the database
        //    await _context.SaveChangesAsync();

        //    // Return the added designation (optional)
        //    return designation;
        //}

    }

}
