using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
   public interface ILeaveSetupRepository : ICommonRepository<LeaveSetup>
    {
        LeaveSetup GetById(int id);
        List<LeaveSetup> GetAll();
        Task<PaginatedResponse<LeaveSetupVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<LeaveSetup> GetMany(Expression<Func<LeaveSetup, bool>> where);

        // list method
        Task<IEnumerable<LeaveSetup>> GetLeaveSetup();

    }
}
