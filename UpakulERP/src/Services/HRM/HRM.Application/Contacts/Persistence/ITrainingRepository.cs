using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.Models.Training;

namespace HRM.Application.Contacts.Persistence
{
    public interface ITrainingRepository : ICommonRepository<Training>
    {
        Training GetById(int id);
        List<Training> GetAll();
        Task<PaginatedListResponse> GetListAsync(int page, int pageSize, string search, string sortColumn, string sortDirection);
        IEnumerable<Training> GetMany(Expression<Func<Training, bool>> where);

    }


}
