using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.BankAccountMapping
{

    public class BankAccountMappingDropdownQueryHandler : IRequestHandler<BankAccountMappingDropdownQuery, List<CustomSelectListItem>>
    {
        IGroupRepository _repository;
        public BankAccountMappingDropdownQueryHandler(IGroupRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<CustomSelectListItem>> Handle(BankAccountMappingDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetAll();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.id == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.GroupId == request.id) ? true : false),
                    Text = s.GroupName,
                    Value = s.GroupId.ToString()
                }));
            }
            return list;
        }

    }
}
