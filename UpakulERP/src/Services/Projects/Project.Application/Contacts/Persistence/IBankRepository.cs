using EF.Core.Repository.Interface.Repository;
using Utility.Domain.DBDomain;

namespace Project.Application.Contacts.Persistence
{
    public interface IBankRepository : ICommonRepository<CommonBank>
    {
        CommonBank GetById(int id);
        void TableActivitiesModification(CommonBank bank);
    }
}
