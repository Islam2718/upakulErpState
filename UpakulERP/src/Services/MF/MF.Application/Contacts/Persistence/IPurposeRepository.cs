using EF.Core.Repository.Interface.Repository;
using MF.Application.Features.DBOrders.Queries.MainPurpose;
using MF.Domain.Models.Loan;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;

namespace MF.Application.Contacts.Persistence
{
    public interface IPurposeRepository : ICommonRepository<Purpose>
    {
        VwPurpose GetByIdXView(int id);
        Purpose GetById(int id);
        //Task<PaginatedPurposeResponse> GetPurposesAsync(int page, int pageSize, string search, string sortColumn, string sortDirection);
        Task<List<PurposeForGridVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
    }
}
