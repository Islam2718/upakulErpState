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

    public class BoardUniversityRepository : CommonRepository<BoardUniversity>, IBoardUniversityRepository
    {
        AppDbContext _context;
        public BoardUniversityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public BoardUniversity GetById(int id)
        {
            var obj = _context.boardUniversitys.FirstOrDefault(c => c.IsActive && c.BUId == id);
            return obj;
        }

        public List<BoardUniversity> GetAll()
        {
            var objlst = _context.boardUniversitys.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<BoardUniversity> GetMany(Expression<Func<BoardUniversity, bool>> where)
        {
            var entities = _context.boardUniversitys.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<BoardUniversityVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "BUName.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "BUId" : sortOrder;
            var query = _context.boardUniversitys.Where(b => b.IsActive)
                .Select(x => new BoardUniversityVM
                {
                    BUId = x.BUId,
                    BUName= x.BUName,
                }).AsQueryable().Where(src_qry, search).OrderBy(sortOrder);

            // Pagination
            var totalRecords = await query.CountAsync();
            var listData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<BoardUniversityVM>(listData, totalRecords);
        }


    }

}
