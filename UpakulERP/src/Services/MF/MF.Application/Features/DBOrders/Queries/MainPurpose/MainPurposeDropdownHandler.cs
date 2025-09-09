using AutoMapper;
using MF.Application.Contacts.Persistence;
using MediatR;
using Utility.Constants;
using Utility.Domain;
using MF.Domain.Models.Loan;
namespace MF.Application.Features.DBOrders.Queries.MainPurpose
{
    public class MainPurposeDropdownHandler : IRequestHandler<MainPurposeDropdownQuery, List<CustomSelectListItem>>
    {
        IMainPurposeRepository _repository;
        IMapper _mapper;

        public MainPurposeDropdownHandler(IMainPurposeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(MainPurposeDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = new List<Purpose>();

            lstObj = _repository.GetMany(x => (x.ParentId ?? 0) == (request.Id ?? 0) && x.RowType == (!x.ParentId.HasValue ? "C" : "S")).ToList();

            var list = new List<CustomSelectListItem> { new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected =true } };

            list.AddRange(lstObj.Select(s => new CustomSelectListItem
            {
                Selected = false,
                Text = s.Name,
                Value = s.Id.ToString()
            }));
            return list;
        }

    }
}
