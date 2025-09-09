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
    public class EducationRepository : CommonRepository<Education>, IEducationRepository
    {
        AppDbContext _context;
        public EducationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        

        public Education GetById(int id)
        {
            var obj = _context.educations.FirstOrDefault(c => c.IsActive && c.EducationId == id);
            return obj;
        }

        public List<Education> GetAll()
        {
            var objlst = _context.educations.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Education> GetMany(Expression<Func<Education, bool>> where)
        {
            var entities = _context.educations.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<EducationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "EducationName.Contains(@0) OR EducationDescription.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "EducationId" : sortOrder;
            var query = _context.educations.Where(b => b.IsActive)
                .Select(x => new EducationVM
                {
                    EducationDescription=x.EducationDescription,
                    EducationId=x.EducationId,
                    EducationName=x.EducationName,
                }).AsQueryable().Where(src_qry, search).OrderBy(sortOrder);

            // Pagination
            var totalRecords = await query.CountAsync();
            var educationList = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<EducationVM>(educationList, totalRecords);
        }


    }
}
