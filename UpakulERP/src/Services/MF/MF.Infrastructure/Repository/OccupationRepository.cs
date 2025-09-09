using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;
using System.Linq.Dynamic.Core;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    class OccupationRepository : CommonRepository<Occupation>, IOccupationRepository
    {
        AppDbContext _context;
        public OccupationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Occupation GetById(int id)
        {
            var obj = _context.occupations.FirstOrDefault(c => c.IsActive && c.OccupationId == id);
            return obj;
        }

        public List<Occupation> GetAll()
        {
            var objlst = _context.occupations.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Occupation> GetMany(Expression<Func<Occupation, bool>> where)
        {
            var entities = _context.occupations.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<OccupationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "OccupationName.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "OccupationId" : sortOrder;
            var query = _context.occupations.Where(b => b.IsActive)
                 .Select(x => new OccupationVM
                 {
                     OccupationName = x.OccupationName,
                     OccupationId = x.OccupationId
                 }).Where(src_qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var obj = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<OccupationVM>(obj, totalRecords);
        }

        public async Task<IEnumerable<Occupation>> GetDeartment()
        {
            var lst = await _context.occupations.Where(c => c.IsActive).ToListAsync();
            return lst;
        }
    }
}
