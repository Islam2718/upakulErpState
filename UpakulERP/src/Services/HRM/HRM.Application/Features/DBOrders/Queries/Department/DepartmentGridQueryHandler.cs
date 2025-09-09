using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
    public class DepartmentGridQueryHandler : IRequestHandler<DepartmentGridQuery, PaginatedResponse<DepartmentVM>>
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentGridQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<DepartmentVM>> Handle(DepartmentGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}
