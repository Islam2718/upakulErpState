using AutoMapper;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Application.Features.DBOrders.Queries.Bank;
using Global.Domain.ViewModels;
using MediatR;
using MessageBroker.Services.Constants;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using Utility.CommonController;
using Utility.Domain;
using Utility.Domain.DBDomain;
using Utility.Response;

namespace Global.API.Controllers
{
    public class BankController : ApiController
    {
        #region Var
        IMediator _mediator;
        private IRabbitMQPublisher<CommonBank> _rabbitMQPublisher;
        private readonly PublisherStatus _publisherStatus;
        IMapper _mapper;
        #endregion Var
        public BankController(IMediator mediator, IOptions<PublisherStatus> publisherStatus, IMapper mapper, IRabbitMQPublisher<CommonBank> rabbitMQPublisher)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publisherStatus = publisherStatus.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateBankCommand request)
        {
            try
            {
                request.CreatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                if(response.StatusCode==HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    var mapObj = _mapper.Map<CommonBank>(request);
                    mapObj.BankId = response.ReturnId.Value;
                    await PublishedMessage(mapObj);
                }
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateBankCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                if(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                {
                    var mapObj = _mapper.Map<CommonBank>(request);
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
        public async Task<IActionResult> Delete([FromBody] DeleteBankCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var bank = await _mediator.Send(new BankByIdQuery(request.BankId));
                var response = await _mediator.Send(request);
                
               
                if (bank != null)
                {
                    var mapObj = _mapper.Map<CommonBank>(bank);
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
        [ProducesResponseType(typeof(BankVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new BankByIdQuery(id));
            return CustomResult(obj);
        }
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<BankVM>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            var query = new BankGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //Get Bank For DropDown
        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBanksForDropdown(int? id)
       {
            try
            {
                var lstObj = await _mediator.Send(new BankDropdownQuery(id ?? 0));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        #region Published Message
        private async Task PublishedMessage(CommonBank obj)
        {
            try
            {
                if (_publisherStatus.IsPublishedAllow)
                    await _rabbitMQPublisher.PublishMessageAsync(obj, RabbitMQQueues.Bank);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Published Message
    }
}
