using Microsoft.EntityFrameworkCore;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Response;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using MF.Domain.Models.View;

namespace MF.Infrastructure.Persistence.Repositories
{
    public class GraceScheduleRepository : CommonRepository<GraceSchedule>, IGraceScheduleRepository
    {
        AppDbContext _context;

        public GraceScheduleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public GraceSchedule GetById(int id)
        {
            var obj = _context.graceschedules.FirstOrDefault(c => c.IsActive && c.Id == id);
            return obj;
        }
        public List<GraceSchedule> GetAll()
        {
            var objlst = _context.graceschedules.Where(c => c.IsActive).ToList();
            return objlst;
        }
        public IEnumerable<GraceSchedule> GetMany(Expression<Func<GraceSchedule, bool>> where)
        {
            var entities = _context.graceschedules.Where(where).Where(b => b.IsActive);
            return entities;
        }
        public async Task<PaginatedResponse<VWGraceSchedule>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search?.Trim();
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;

            string src_qry;

            if (string.IsNullOrEmpty(search))
            {
                // Always true filter (no search)
                src_qry = "@0 == @0";
            }
            else if (bool.TryParse(search, out var boolVal))
            {
                // Search boolean field IsApproved
                src_qry = "IsApproved == @0";
                search = boolVal.ToString();
            }
            else
            {
                // Search string fields only
                src_qry = "Reason != null && Reason.Contains(@0) || " +
                          "Office != null && Office.Contains(@0) || " +
                          "Group != null && Group.Contains(@0) || " +
                          "ApplicationNo != null && ApplicationNo.Contains(@0)";
            }

            var query = _context.vwGraceSchedules
                .Select(x => new VWGraceSchedule
                {
                    Id = x.Id,
                    Reason = x.Reason,
                    Office = x.Office,
                    Group = x.Group,
                    ApplicationNo = x.ApplicationNo,
                    GraceFrom = x.GraceFrom,
                    GraceTo = x.GraceTo,
                    IsApproved = x.IsApproved
                })
                .Where(src_qry, search)
                .OrderBy(sortOrder)
                .AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var obj = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<VWGraceSchedule>(obj, totalRecords);
        }


        public async Task<IEnumerable<GraceSchedule>> GetGraceSchedule()
        {
            return await _context.graceschedules
                .Where(x => x.IsActive)
                .ToListAsync();
        }
    }
    }

