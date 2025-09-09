using MediatR;
using Utility.Domain;

namespace MF.Application.Features.DBOrders.Queries.MRAPurpose
{
    public class MRAPurposeDropdownQuery : IRequest<List<CustomSelectListItem>>
    {
        public string Category { get; set; }
        public string Subcategory { get; set; }
        
        public MRAPurposeDropdownQuery(string category,string subcategory)
        {
            this.Category = category;
            this.Subcategory = subcategory;
        }
    }


}
