using MediatR;
using MF.Application.Contacts.Persistence;
using Utility.Constants;
using Utility.Domain;
using static Utility.Enums.OfficeType;

namespace MF.Application.Features.DBOrders.Queries.Designation
{
    public class DesignationDropdownQueryHandler : IRequestHandler<DesignationDropdownQuery, List<CustomSelectListItem>>
    {
        IDesignationRepository _repository;
        public DesignationDropdownQueryHandler(IDesignationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomSelectListItem>> Handle(DesignationDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });
            list.AddRange(_repository.GetOfficeLevelWiseDesignation(request.officeId,0/*(int) OfficeTypeEnum.Zonal*/)
                .Select(s => new CustomSelectListItem
            {
                Text = (!string.IsNullOrWhiteSpace(s.DesignationCode) ? $"({s.DesignationCode}) " : "") + s.DesignationName,
                Value = s.DesignationId.ToString()
            }));
            return list;
        }

    }
}
