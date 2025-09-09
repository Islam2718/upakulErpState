using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UpakulHRM.Infrastructure.Persistence;
using Utility.Response;

namespace HRM.Infrastructure.Repository
{
    public class OfficeTypeXConfigureDetailsRepository : CommonRepository<OfficeTypeXConfigureDetails>, IOfficeTypeXConfigureDetailsRepository
    {
        AppDbContext _context;

        public OfficeTypeXConfigureDetailsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(List<OfficeTypeXConfigureDetails> details)
        {
            await _context.OfficeTypeXConfigureDetails.AddRangeAsync(details);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
        public OfficeTypeXConfigureDetails GetById(int id)
        {
            return _context.OfficeTypeXConfigureDetails.FirstOrDefault(x => x.Id == id);
        }

        public List<OfficeTypeXConfigureDetails> GetAll()
        {
            return _context.OfficeTypeXConfigureDetails.ToList();
        }
        public IEnumerable<OfficeTypeXConfigureDetails> GetMany(Expression<Func<OfficeTypeXConfigureDetails, bool>> where)
        {
            return _context.OfficeTypeXConfigureDetails.Where(where);
        }
        public async Task<PaginatedResponse<OfficeTypeXConfigureDetailsVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "LeaveCategoryId.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "ConfigureMasterId" : sortOrder;

            var query = _context.OfficeTypeXConfigureDetails
                .Select(x => new OfficeTypeXConfigureDetailsVM
                {
                    ApproverDesignationId = x.ApproverDesignationId,
                    MinimumLeave = x.MinimumLeave,
                    MaximumLeave = x.MaximumLeave
                })
                .AsQueryable()
                .Where(src_qry, search)
                .OrderBy(sortOrder);

            var totalRecords = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<OfficeTypeXConfigureDetailsVM>(items, totalRecords);
        }
        public async Task<IEnumerable<OfficeTypeXConfigureDetails>> GetOfficeTypeXConfigureDetails()
        {
            return await _context.OfficeTypeXConfigureDetails.ToListAsync();
        }
    }
}