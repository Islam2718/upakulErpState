using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Infrastructure.Persistence;

namespace MF.Infrastructure.Repository
{
    public class DailyProcessRepository : CommonRepository<DailyProcess>, IDailyProcessRepository
    {
        AppDbContext _context;

        public DailyProcessRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public DailyProcess GetById(int id)
        {
            var obj = _context.dailyProcess.FirstOrDefault(c => c.IsActive);
            return obj;
        }       

        public IEnumerable<DailyProcess> GetMany(Expression<Func<DailyProcess, bool>> where)
        {
            var entities = _context.dailyProcess.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public IEnumerable<HolyDay> CheckHolidays(Expression<Func<HolyDay, bool>> where)
        {
            var entities = _context.holidays.Where(where).Where(b => b.IsActive);
            return entities;
        }
    }
}
