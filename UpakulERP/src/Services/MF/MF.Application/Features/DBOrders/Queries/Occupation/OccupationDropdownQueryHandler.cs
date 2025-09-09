using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.Occupation
{

    public class OccupationDropdownQueryHandler : IRequestHandler<OccupationDropdownQuery, List<CustomSelectListItem>>
    {
        IOccupationRepository _repository;
        IMapper _mapper;

        public OccupationDropdownQueryHandler(IOccupationRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(OccupationDropdownQuery request, CancellationToken cancellationToken)
        {
            var lstObj = _repository.GetAll();
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = (request.id == 0 ? true : false) });
            if (lstObj.Any())
            {
                list.AddRange(lstObj.Select(s => new CustomSelectListItem
                {
                    Selected = ((s.OccupationId == request.id) ? true : false),
                    Text = s.OccupationName,
                    Value = s.OccupationId.ToString()
                }));
            }
            return list;
        }

    }



}
