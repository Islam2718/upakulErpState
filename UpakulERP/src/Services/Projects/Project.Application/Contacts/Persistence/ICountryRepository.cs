using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using Utility.Domain.DBDomain;

namespace Project.Application.Contacts.Persistence
{
   public interface ICountryRepository : ICommonRepository<CommonCountry>
   {
        List<CommonCountry> GetAll();
    }
}
