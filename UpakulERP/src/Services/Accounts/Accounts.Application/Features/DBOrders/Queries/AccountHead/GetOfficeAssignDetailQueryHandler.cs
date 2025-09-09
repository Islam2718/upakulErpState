using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Domain.ViewModel;
using MediatR;

namespace Accounts.Application.Features.DBOrders.Queries.AccountHead
{
    public class GetOfficeAssignDetailQueryHandler : IRequestHandler<GetOfficeAssignDetailQuery, List<HeadXOfficeAssignVM>> 
    {
        IAccountHeadRepository _repository;
        public GetOfficeAssignDetailQueryHandler(IAccountHeadRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<HeadXOfficeAssignVM>> Handle(GetOfficeAssignDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetOfficeAssignDetails((request.officeid??0), (request.accountId??0));
        }
    }
}
