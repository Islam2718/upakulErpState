using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.MainPurpose
{
    public class MainPurposeDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public int? Id { get; set; }
        public MainPurposeDropdownQuery(int? pId)
        {
            Id = pId;
        }
        /*
        public string SearchText { get; set; }
        public MainPurposeDropdownQuery(int Id, string searchText)
        {
            Id = Id;
            SearchText = searchText;
        }
        */
    }


}
