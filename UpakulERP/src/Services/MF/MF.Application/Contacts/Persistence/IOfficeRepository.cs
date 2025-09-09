using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.ViewModels;
using Utility.Domain.DBDomain;

namespace MF.Application.Contacts.Persistence
{
    public interface IOfficeRepository : ICommonRepository<CommonOffice>
    {
        Task<IEnumerable<CommonOffice>> GetOfficeByParentId(int pid);
        IEnumerable<OfficeForDropDownVM> GetOfficeDropdown(int officeId, int officeType);
    }
}
