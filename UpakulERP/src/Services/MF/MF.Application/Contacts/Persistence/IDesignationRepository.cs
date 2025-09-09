using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using Utility.Domain.DBDomain;

namespace MF.Application.Contacts.Persistence
{
    public interface IDesignationRepository : ICommonRepository<CommonDesignation>
    {
        List<CommonDesignation> GetOfficeLevelWiseDesignation(int officeId, int officeTypeid);
    }


}
