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
using System.Threading.Tasks;
using UpakulHRM.Infrastructure.Persistence;
using Utility.Enums.HRM;
using Utility.Response;

namespace HRM.Infrastructure.Repository
{

    public class LeaveSetupRepository : CommonRepository<LeaveSetup>, ILeaveSetupRepository
    {
        private readonly AppDbContext _context;

        public LeaveSetupRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public LeaveSetup GetById(int id)
        {
            var obj = _context.leavesetups.FirstOrDefault(c => c.IsActive && c.LeaveTypeId == id); // removed IsActive
            return obj;
        }

        public List<LeaveSetup> GetAll()
        {
            return _context.leavesetups.Where(c => c.IsActive).ToList();
        }

        public IEnumerable<LeaveSetup> GetMany(Expression<Func<LeaveSetup, bool>> where)
        {
            return _context.leavesetups.Where(where).Where(b => b.IsActive);
        }


        public async Task<PaginatedResponse<LeaveSetupVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "LeaveTypeName.Contains(@0) OR LeaveCategoryId.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "LeaveTypeId" : sortOrder;
            var query = _context.leavesetups
                      .Where(b => b.IsActive)
                      .Where(src_qry, search)
                      .OrderBy(sortOrder);

            var vmQuery = query.Select(x => new LeaveSetupVM
            {
                LeaveTypeId = x.LeaveTypeId,
                LeaveCategoryId = x.LeaveCategoryId,
                LeaveTypeName = x.LeaveTypeName,
                EmployeeTypeId = x.EmployeeTypeId,
                EffectiveStartDate = x.EffectiveStartDate,
                EffectiveEndDate = x.EffectiveEndDate
            });
            var totalRecords = await vmQuery.CountAsync();
            var items = await vmQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<LeaveSetupVM>(items, totalRecords);
        }

        public async Task<IEnumerable<LeaveSetup>> GetLeaveSetup()
        {
            var leavesetuplst = await _context.leavesetups.Where(c => c.IsActive).ToListAsync();
            return leavesetuplst;
        }
    }
}