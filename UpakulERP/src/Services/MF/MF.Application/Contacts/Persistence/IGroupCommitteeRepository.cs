using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Domain;
using Utility.Domain.DBDomain;

namespace MF.Application.Contacts.Persistence
{
    public interface IGroupCommitteeRepository : ICommonRepository<GroupCommittee>
    {
        //GroupCommittee GetById(int id);
        List<GroupCommitteeVM> GetActiveCommitteeMember(int groupId);

        Task<bool> AddRangeAsync(CreateGroupCommitteeCommand entities);

        //IEnumerable<GroupCommittee> GetMany(Expression<Func<GroupCommittee, bool>> where);
    }
}
