using EF.Core.Repository.Interface.Repository;
using MF.Application.Features.DBOrders.Queries.MasterComponent;
using MF.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
   public interface IMasterComponentRepository : ICommonRepository<MasterComponent>
   {

        MasterComponent GetById(int id);
        List<MasterComponent> GetAll();
        Task<PaginatedResponse<MasterComponentVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<MasterComponent> GetMany(Expression<Func<MasterComponent, bool>> where);
        // insert method
        Task<MasterComponent> Add(MasterComponent obj);
        Task<bool> AddAsync(MasterComponent obj);
    }
}

