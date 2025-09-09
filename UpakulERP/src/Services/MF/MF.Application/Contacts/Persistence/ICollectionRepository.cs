using MF.Domain.ViewModels.Collection;

namespace MF.Application.Contacts.Persistence
{
    public interface ICollectionRepository
    {
        Task<List<EmployeeXGroupCollectionVM>> EmployeeXGroupCollectionInfo(int officeId, int employeeId, DateTime transactionDate);
        Task<List<GroupXMemberCollectionVM>>GroupXMemberCollectionInfo(int officeId, int employeeId, DateTime transactionDate, int groupId);
    }
}
