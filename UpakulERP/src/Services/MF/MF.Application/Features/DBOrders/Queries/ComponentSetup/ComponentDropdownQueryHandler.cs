using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.Component;
using Utility.Constants;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.ComponentSetup
{
   public class ComponentDropdownQueryHandler : IRequestHandler<ComponentDropdownQuery, List<CustomSelectListItem>>
    {
        IComponentRepository _repository;
        IMapper _mapper;

        public ComponentDropdownQueryHandler (IComponentRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CustomSelectListItem>> Handle(ComponentDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();

            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });

            list.AddRange(_repository.GetOfficeXComponent(request.officeId??0,request.ComponentType,request.LoanType).Select(s => new CustomSelectListItem
            {
                Text = s.ComponentName,
                Value = s.Id.ToString()
            }));
            return list;
        }

    }
}


