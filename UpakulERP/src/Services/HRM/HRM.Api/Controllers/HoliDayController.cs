using AutoMapper;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Application.Features.DBOrders.Queries.HoliDay;
using HRM.Domain.ViewModels;
using MediatR;
using MessageBroker.Services.Constants;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using Utility.CommonController;
using Utility.Domain.DBDomain;

namespace HRM.Api.Controllers
{
    public class HoliDayController : ApiController
    {
        #region Var
        IMediator _mediator;
        IMapper _mapper;
        private readonly PublisherStatus _publisherStatus;
        private IRabbitMQPublisher<CommonHoliday> _rabbitMQPublisher;
        #endregion Var
        public HoliDayController(IMediator mediator, IMapper mapper ,IOptions<PublisherStatus> publisherStatus, IRabbitMQPublisher<CommonHoliday> rabbitMQPublisher)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publisherStatus = publisherStatus.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateHoliDayCommand request)
        {
            try
            {

                request.CreatedBy = loggedInEmployeeId;
                request.CreatedOn = DateTime.Now;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    var mapObj = _mapper.Map<CommonHoliday>(request);
                    mapObj.HolidayId = response.ReturnId.Value;
                    mapObj.IsActive = true;
                    await PublishedMessage(mapObj);
                }
                    return CustomResult(response.Message, response.StatusCode);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(HolidayVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new HolidayByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateHoliDayCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                request.UpdatedOn = DateTime.Now;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                {
                    var mapObj = _mapper.Map<CommonHoliday>(request);
                    mapObj.IsActive = true;
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
        public async Task<IActionResult> Delete([FromBody] DeleteHoliDayCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var obj = await _mediator.Send(new HolidayByIdQuery(request.HoliDayId));
                var response = await _mediator.Send(request);
                if (obj != null)
                {
                    var mapObj = _mapper.Map<CommonHoliday>(obj);
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
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            // ✅ Debugging log
            //Console.WriteLine($"Received Parameters -> Page: {page}, PageSize: {pageSize}, Search: '{search}', SortColumn: '{sortColumn}', SortDirection: '{sortDirection}'");
            var query = new HolidayGridQuery
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
        private async Task PublishedMessage(CommonHoliday obj)
        {
            try
            {
                if (_publisherStatus.IsPublishedAllow)
                    await _rabbitMQPublisher.PublishMessageAsync(obj, RabbitMQQueues.Holiday);
            }
            catch (Exception)
            {

            }
        }
        #endregion Published Message
    }
}
