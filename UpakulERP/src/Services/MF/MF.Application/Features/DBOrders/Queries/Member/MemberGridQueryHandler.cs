using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models.View;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    public class MemberGridQueryHandler : IRequestHandler<MemberGridQuery, PaginatedResponse<VWmemberCommonData>>
    {
        private readonly IMemberRepository _repository;

        public MemberGridQueryHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<VWmemberCommonData>> Handle(MemberGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder, request.logedInOfficeId);
        }
    }
}
