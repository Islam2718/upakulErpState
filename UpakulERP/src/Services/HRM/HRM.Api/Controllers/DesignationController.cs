using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Application.Features.DBOrders.Queries.Designation;
using Utility.Domain;
using HRM.Domain.ViewModels;
using AutoMapper;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using Utility.Domain.DBDomain;
using Microsoft.Extensions.Options;
using MessageBroker.Services.Constants;

namespace Global.API.Controllers
{

    public class DesignationController : ApiController
    {
        #region Var
        IMediator _mediator;
        IMapper _mapper;
        private readonly PublisherStatus _publisherStatus;
        private IRabbitMQPublisher<CommonDesignation> _rabbitMQPublisher;
        #endregion Var
        public DesignationController(IMediator mediator, IOptions<PublisherStatus> publisherStatus, IMapper mapper, IRabbitMQPublisher<CommonDesignation> rabbitMQPublisher)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publisherStatus = publisherStatus.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDesignationCommand request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.DesignationName) )
                    return CustomResult("Designation is required.", HttpStatusCode.BadRequest);
                else if(request.OrderNo == 0)
                    return CustomResult("Order number is required.", HttpStatusCode.BadRequest);
                else
                {
                    request.CreatedBy = loggedInEmployeeId;
                    var response = await _mediator.Send(request);
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                    {
                        var mapObj = _mapper.Map<CommonDesignation>(request);
                        mapObj.DesignationId = response.ReturnId.Value;
                        mapObj.IsActive = true;
                        await PublishedMessage(mapObj);
                    }
                        return CustomResult(response.Message, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(DesignationVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new DesignationByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDesignationCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                {
                    var mapObj = _mapper.Map<CommonDesignation>(request);
                    mapObj.IsActive=true;
                    await PublishedMessage(mapObj);
                }
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] DeleteDesignationCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var obj = await _mediator.Send(new DesignationByIdQuery(request.DesignationId));
                var response = await _mediator.Send(request);
                if (obj != null)
                {
                    var mapObj = _mapper.Map<CommonDesignation>(obj);
                    mapObj.IsActive = false;
                    await PublishedMessage(mapObj);
                }
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DesignationDropdown()
        {
            var lst = await _mediator.Send(new DesignationDropdownQuery());
            return CustomResult(lst);
        }

        [HttpGet]
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            // ✅ Debugging log
            //Console.WriteLine($"Received Parameters -> Page: {page}, PageSize: {pageSize}, Search: '{search}', SortColumn: '{sortColumn}', SortDirection: '{sortDirection}'");
            var query = new DesignationGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #region Published Message
        private async Task PublishedMessage(CommonDesignation obj)
        {
            try
            {
                if (_publisherStatus.IsPublishedAllow)
                    await _rabbitMQPublisher.PublishMessageAsync(obj, RabbitMQQueues.Designation);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Published Message
    }
}
