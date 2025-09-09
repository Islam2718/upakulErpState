using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using UpakulHRM.Infrastructure.Persistence;
using Utility.Response;
using HRM.Domain.Models.Views;

namespace HRM.Infrastructure.Repository
{

    public class HoliDayRepository : CommonRepository<HoliDay>, IHoliDayRepository
    {
        AppDbContext _context;
        public HoliDayRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public HoliDay GetById(int id)
        {
            var obj = _context.holidays.FirstOrDefault(c => c.IsActive && c.HolidayId == id);
            return obj;
        }

        public List<HoliDay> GetAll()
        {
            var objlst = _context.holidays.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<HoliDay> GetMany(Expression<Func<HoliDay, bool>> where)
        {
            var entities = _context.holidays.Where(where).Where(b => b.IsActive);
            return entities;
        }
        public async Task<PaginatedResponse<VWHoliday>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "HolidayName.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "HolidayId" : sortOrder;
            var query = _context.vwHolidays
                .Select(x => new VWHoliday
                {
                    EndDate = x.EndDate,
                    HolidayId = x.HolidayId,
                    HolidayType=x.HolidayType,
                    HolidayName= x.HolidayName,
                    StartDate=x.StartDate
                }).AsQueryable().Where(src_qry, search).OrderBy(sortOrder);

            // Pagination
            var totalRecords = await query.CountAsync();
            var holidays = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<VWHoliday>(holidays, totalRecords);
        }

        public async Task<IEnumerable<HoliDay>> GetHoliDay()
        {
            var holidaylst = await _context.holidays.Where(c => c.IsActive).ToListAsync();
            return holidaylst;
        }

        //public async Task<HolyDay> AddHolyDay(HolyDay holyday)
        //{
        //    await _context.holydays.AddAsync(holyday);

        //    // Save the changes to the database
        //    await _context.SaveChangesAsync();
        //    return holyday;
        //}

    }

}
