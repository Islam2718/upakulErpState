using AutoMapper;
using MF.Application.Contacts.Persistence;
using MediatR;
using Utility.Constants;
using Utility.Domain;
namespace MF.Application.Features.DBOrders.Queries.MRAPurpose
{
    public class MRAPurposeDropdownHandler : IRequestHandler<MRAPurposeDropdownQuery, List<CustomSelectListItem>>
    {
        IMRAPurposeRepository _repository;
        IMapper _mapper;

        public MRAPurposeDropdownHandler(IMRAPurposeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(MRAPurposeDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetMany(x => (x.Category ?? "") == request.Category && (x.Subcategory ?? "") == (request.Subcategory??"")).ToList();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true});
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = false,
                    Text = s.Code + " - " + s.Category + ((s.Subcategory ?? "") == "" ? "" : " > " + s.Subcategory) + ((s.Name ?? "") == "" ? "" : " > " + s.Name),
                    Value = s.Code.ToString()
                }));
            }
            return list;
        }

    }
}
