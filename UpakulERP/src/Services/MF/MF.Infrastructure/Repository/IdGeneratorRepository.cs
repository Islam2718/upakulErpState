using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace MF.Infrastructure.Repository
{
 public   class IdGeneratorRepository : CommonRepository<IdGenerate>, IIdGeneratorRepository
    {
        AppDbContext _context;
        public IdGeneratorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IdGenerate GetById(int id)
        {
            var obj = _context.codeGenerator.FirstOrDefault(c => c.Id == id);
            return obj;
        }
        public List<IdGenerate> GetAll()
        {
            var objlst = _context.codeGenerator.OrderBy(x => x.Id).ToList();
            return objlst;
        }
     
        public IEnumerable<IdGenerate> GetMany(Expression<Func<IdGenerate, bool>> where)
        {
            var entities = _context.codeGenerator.Where(where);
            return entities;
        }


        public async Task<IdGenerate> Add(IdGenerate obj)
        {
            // Add the new row to the context
            await _context.codeGenerator.AddAsync(obj);

            // Save the changes to the database
            await _context.SaveChangesAsync();
            return obj;
        }

    }
}
