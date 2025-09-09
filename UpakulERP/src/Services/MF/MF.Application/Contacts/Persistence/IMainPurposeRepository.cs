using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MF.Application.Contacts.Persistence
{
    public interface IMainPurposeRepository : ICommonRepository<Purpose>
    {
        // MRAPurpose GetById(int id);
        IEnumerable<Purpose> GetMany(Expression<Func<Purpose, bool>> where);
       // IEnumerable<MRAPurpose> GetMany(Expression<Func<MRAPurpose, bool>> where);
    }
}
