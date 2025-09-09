using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.DBOrders.Queries.Country;
using Utility.CommonController;
using Utility.Domain;

namespace Project.API.Controllers
{
    public class CountryController : ApiController
    {

        #region Var
        IMediator _mediator;
        IMapper _mapper;
        #endregion Var

        public CountryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCountryForDropdown(int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new CountryDropdownQuery(id ?? 0));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
