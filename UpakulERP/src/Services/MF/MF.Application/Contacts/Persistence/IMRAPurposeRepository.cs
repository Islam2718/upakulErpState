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
    public interface IMRAPurposeRepository : ICommonRepository<MRAPurpose>
    {
       // MRAPurpose GetById(int id);
        //List<MRAPurpose> GetAll();
        IEnumerable<MRAPurpose> GetMany(Expression<Func<MRAPurpose, bool>> where);
    }
}
