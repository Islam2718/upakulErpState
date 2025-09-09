using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{

    public interface IGraceScheduleRepository : ICommonRepository<GraceSchedule>
    {
        GraceSchedule GetById(int id);
        List<GraceSchedule> GetAll();
        Task<PaginatedResponse<VWGraceSchedule>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<GraceSchedule> GetMany(Expression<Func<GraceSchedule, bool>> where);

        // list method
        Task<IEnumerable<GraceSchedule>> GetGraceSchedule();
    }
}
