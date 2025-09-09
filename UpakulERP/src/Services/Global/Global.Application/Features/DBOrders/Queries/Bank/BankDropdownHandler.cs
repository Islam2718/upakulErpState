using Global.Application.Contacts.Persistence;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace Global.Application.Features.DBOrders.Queries.Bank
{
    public class BankDropdownHandler : IRequestHandler<BankDropdownQuery, List<CustomSelectListItem>>
    {
        IBankRepository _repository;
        public BankDropdownHandler(IBankRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<CustomSelectListItem>> Handle(BankDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj =  _repository.GetAll();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.id == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.BankId == request.id) ? true : false),
                    Text = s.BankShortCode+" - "+s.BankName,
                    Value = s.BankId.ToString()
                }));
            }
            return list;
        }

    }
}
