using System.Net;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Domain.ViewModels;
using HRM.Application.Features.DBOrders.Queries.Employee;
using MediatR;
using HRM.Application.Features.DBOrders.Queries.Department;
using Utility.Domain.DBDomain;
using MessageBroker.Services.Contacts.Persistence;
using MessageBroker.Services.Settings;
using AutoMapper;
using Microsoft.Extensions.Options;
using MessageBroker.Services.Constants;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Domain.Models;
using static Utility.Enums.HRM.EmployeeStatus;

namespace HRM.Api.Controllers
{
    public class EmployeeController : ApiController
    {

        #region Var
        IMediator _mediator;
        IMapper _mapper;
        private readonly PublisherStatus _publisherStatus;
        private IRabbitMQPublisher<CommonEmployee> _rabbitMQPublisher;
        #endregion Var
        public EmployeeController(IMediator mediator, IOptions<PublisherStatus> publisherStatus, IMapper mapper, IRabbitMQPublisher<CommonEmployee> rabbitMQPublisher)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publisherStatus = publisherStatus.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeCommand request)
        {
            try
            {
                request.CreatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    var employee = await _mediator.Send(new EmployeeByIdViewQuery(response.ReturnId.Value));
                    var mapObj = _mapper.Map<CommonEmployee>(employee);
                    mapObj.EmployeeFullName = employee.FirstName + (string.IsNullOrEmpty(employee.LastName) ? "" : " ") + employee.LastName ?? "";
                    mapObj.DesignationId = employee.DesignationId;
                    mapObj.IsActive = true;
                    await PublishedMessage(mapObj);
                }
                return CustomResult(response.Message, new { id = response.ReturnId }, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromForm] UpdateEmployeeCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                {
                    var employee = await _mediator.Send(new EmployeeByIdViewQuery(request.EmployeeId));
                    var mapObj = _mapper.Map<CommonEmployee>(employee);
                    if (employee.EmployeeStatus == EmployeeStatusEnum.Active.ToString() || employee.EmployeeStatus == EmployeeStatusEnum.SalaryHeldUp.ToString())
                        mapObj.IsActive = true;
                    else mapObj.IsActive = false;
                    mapObj.EmployeeFullName = employee.FirstName + (string.IsNullOrEmpty(employee.LastName) ? "" : " ") + employee.LastName ?? "";
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
        public async Task<IActionResult> Delete([FromBody] DeleteEmployeeCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var employee = await _mediator.Send(new EmployeeByIdViewQuery(request.Employeeid));
                var response = await _mediator.Send(request);


                if (employee != null)
                {
                    var mapObj = _mapper.Map<CommonEmployee>(employee);
                    mapObj.EmployeeFullName = employee.FirstName + (string.IsNullOrEmpty(employee.LastName) ? "" : " ") + employee.LastName ?? "";
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
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new EmployeeByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MultipleDropdownForEmployeeProfileVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllDropDownData()
        {
            var obj = await _mediator.Send(new AllEmployeeProfileDropdownQuery(loggedInOfficeId ?? 0));
            return CustomResult(obj);
        }

        [HttpGet]
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            var query = new EmployeeGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
                OfficeId=loggedInOfficeId.Value
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #region Published Message
        private async Task PublishedMessage(CommonEmployee obj)
        {
            try
            {
                if (_publisherStatus.IsPublishedAllow)
                    await _rabbitMQPublisher.PublishMessageAsync(obj, RabbitMQQueues.Employee);
            }
            catch (Exception)
            {

            }
        }
        #endregion Published Message
    }
}
