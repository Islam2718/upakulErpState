using System.Net;
using AutoMapper;
using Global.Application.Features.DBOrders.Commands.Create.Command;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Application.Features.DBOrders.Queries.GeoLocation;
using Global.Domain.ViewModels;
using MediatR;
using MessageBroker.Services.Constants;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Utility.CommonController;
using Utility.Domain;
using Utility.Domain.DBDomain;
using Utility.Enums;

namespace Global.API.Controllers
{
    public class GeoLocationController : ApiController
    {
        #region Var
        IMediator _mediator;
        private IRabbitMQPublisher<CommonGeoLocation> _rabbitMQPublisher;
        private readonly PublisherStatus _publisherStatus;
        IMapper _mapper;
        #endregion Var

        public GeoLocationController(IMediator mediator, IOptions<PublisherStatus> publisherStatus, IMapper mapper, IRabbitMQPublisher<CommonGeoLocation> rabbitMQPublisher)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publisherStatus = publisherStatus.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateGeoLoactionCommand request)
        {
            try
            {
                if (request.GeoLocationType > (int)GeoLocationType.GeoLocationTypeEnum.Division && (request.ParentId ?? 0) == 0)
                    return CustomResult("Parent location id is required.", HttpStatusCode.BadRequest);
                else
                {
                    request.CreatedBy = loggedInEmployeeId;
                    var response = await _mediator.Send(request);
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                    {
                        var mapObj = _mapper.Map<CommonGeoLocation>(request);
                        mapObj.GeoLocationId = response.ReturnId.Value;
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

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateGeoLocationCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                {
                    var mapObj = _mapper.Map<CommonGeoLocation>(request);
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
        public async Task<IActionResult> Delete([FromBody] DeleteGeoLocationCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var obj = await _mediator.Send(new GeoLocationByIdQuery(request.GeoLocationId));
                var response = await _mediator.Send(request);
                if (obj != null)
                {
                    var mapObj = _mapper.Map<CommonGeoLocation>(obj);
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
        [ProducesResponseType(typeof(GeoLocationVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new GeoLocationByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeoLocationByParentId(int? parentId)
        {
            try
            {
                var lstObj = await _mediator.Send(new GeoLocationDropdownQuery(parentId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
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
            var query = new GeoLocationGridQuery
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
        private async Task PublishedMessage(CommonGeoLocation obj)
        {
            try
            {
                if (_publisherStatus.IsPublishedAllow)
                    await _rabbitMQPublisher.PublishMessageAsync(obj, RabbitMQQueues.GeoLocation);
            }
            catch (Exception ex)
            {

            }
            
        }
        #endregion Published Message
    }
}
