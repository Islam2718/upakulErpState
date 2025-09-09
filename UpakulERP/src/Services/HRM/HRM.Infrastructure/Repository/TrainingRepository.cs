using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models.Training;
using Microsoft.EntityFrameworkCore;
using UpakulHRM.Infrastructure.Persistence;

namespace HRM.Infrastructure.Repository
{
    public class TrainingRepository : CommonRepository<Training>, ITrainingRepository
    {
        AppDbContext _context;
        public TrainingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Training GetById(int id)
        {
            var obj = _context.trainings.FirstOrDefault(c => c.IsActive && c.TrainingId == id);
            return obj;
        }

        public List<Training> GetAll()
        {
            var objlst = _context.trainings.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Training> GetMany(Expression<Func<Training, bool>> where)
        {
            var entities = _context.trainings.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedListResponse> GetListAsync(int page, int pageSize, string search, string sortColumn, string sortDirection)
        {
            var query = _context.trainings.Where(b => b.IsActive).AsQueryable();

            // Searching
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Title.Contains(search));
            }

            // Sorting
            if (typeof(Training).GetProperty(sortColumn) != null)
            {
                query = sortDirection.ToLower() == "asc"
                ? query.OrderBy(x => x.Title)
                : query.OrderByDescending(x => x.Title);
            }

            // Pagination
            var totalRecords = await query.CountAsync();
            var listData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedListResponse(listData, totalRecords);
        }


    }


}
