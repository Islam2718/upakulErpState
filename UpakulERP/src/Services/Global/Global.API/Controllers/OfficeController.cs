using Global.Application.Features.DBOrders.Queries.GeoLocation;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Enums;
using Utility.Domain;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Application.Features.DBOrders.Queries.Office;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using AutoMapper;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using Utility.Domain.DBDomain;
using Microsoft.Extensions.Options;
using MessageBroker.Services.Constants;
using Global.Domain.ViewModels;

namespace Global.API.Controllers
{
    public class OfficeController : ApiController
    {
        #region Var
        IMediator _mediator;
        private IRabbitMQPublisher<CommonOffice> _rabbitMQPublisher;
        private readonly PublisherStatus _publisherStatus;
        IMapper _mapper;
        #endregion Var

        public OfficeController(IMediator mediator, IOptions<PublisherStatus> publisherStatus, IMapper mapper, IRabbitMQPublisher<CommonOffice> rabbitMQPublisher)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publisherStatus = publisherStatus.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateOfficeCommand request)
        {
            try
            {
                if (request.ParentId == 0)
                    return CustomResult("Parent id is required.", HttpStatusCode.BadRequest);
                else
                {
                    request.CreatedBy = loggedInEmployeeId;
                    request.CreatedOn = DateTime.Now;
                    var response = await _mediator.Send(request);
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                    {
                        var mapObj = _mapper.Map<CommonOffice>(request);
                        mapObj.OfficeId = response.ReturnId.Value;
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
        public async Task<IActionResult> Update(UpdateOfficeCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                request.UpdatedOn = DateTime.Now;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                {
                    var mapObj = _mapper.Map<CommonOffice>(request);
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
        public async Task<IActionResult> Delete([FromBody] DeleteOfficeCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var obj = await _mediator.Send(new OfficeByIdQuery(request.OfficeId));
                var response = await _mediator.Send(request);
                if (obj != null)
                {
                    var mapObj = _mapper.Map<CommonOffice>(obj);
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

        //[HttpGet]
        //[ProducesResponseType(typeof(GeoLocationVM), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var obj = await _mediator.Send(new GeoLocationByIdQuery(id));
        //    return CustomResult(obj);
        //}


        [HttpGet]
        [ProducesResponseType(typeof(OfficeVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var obj = await _mediator.Send(new OfficeByIdQuery(id));
                return CustomResult(obj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(OfficeVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeById(int id)
        {
            var obj = await _mediator.Send(new OfficeByIdQuery(id));
            return CustomResult(obj);
        }

        #region For Office Drop Down
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeByParentId(int? parentId)
        {
            try
            {
                var lstObj = await _mediator.Send(new OfficeQuery(parentId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBranchOfficeDropdown()
        {
            try
            {
                var lstObj = await _mediator.Send(new OfficeDropdownQuery(loggedInOfficeId ?? 0, (int)OfficeType.OfficeTypeEnum.Branch));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet, ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeDropdown()
        {
            try
            {
                var lstObj = await _mediator.Send(new OfficeDropdownQuery(loggedInOfficeId ?? 0));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion For Office Drop Down



        [HttpGet]
        public async Task<IActionResult> LoadGrid(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 0,
            [FromQuery] string search = "",
            [FromQuery] string SortOrder = "")
        {
            var query = new OfficesGridQuery
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
        private async Task PublishedMessage(CommonOffice obj)
        {
            if (_publisherStatus.IsPublishedAllow)
                await _rabbitMQPublisher.PublishMessageAsync(obj, RabbitMQQueues.Office);
        }
        #endregion Published Message
    }
}
