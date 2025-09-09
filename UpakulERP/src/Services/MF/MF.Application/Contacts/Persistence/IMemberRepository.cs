using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Utility.Domain;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
    public interface IMemberRepository : ICommonRepository<Member>
    {
        Member GetById(int id);
        VWMember GetMemberDetailById(int id);
        string MemberDataCheck(string NationalId, string SmartCard, string ContactNoOwn);
        IEnumerable<Member> GetMany(Expression<Func<Member, bool>> where);
        Task<PaginatedResponse<VWmemberCommonData>> LoadGrid(int page, int pageSize, string search, string sortOrder,int logedinOfficeId);
        Task<MultipleDropdownForMemberProfileVM> AllMemberProfileDropDown(int officeId);
        List<CustomSelectListItem> GetMemberByGroupIdDropdown(int groupId);
        Task<List<GroupXMemberComponentDetailsVM>> GroupXMemberComponentDetails(int logedinOfficeId, int groupId);
    }
}
