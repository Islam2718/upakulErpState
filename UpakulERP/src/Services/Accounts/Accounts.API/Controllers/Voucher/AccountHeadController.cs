using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using Accounts.Application.Features.DBOrders.Commands.Delete.Command;
using Accounts.Application.Features.DBOrders.Commands.Update.Commands;
using Accounts.Application.Features.DBOrders.Queries.AccountHead;
using Accounts.Domain.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Constants;
using Utility.Response;

namespace Accounts.API.Controllers.Voucher
{
    public class AccountHeadController : ApiController
    {
        IMediator _mediator;
        public AccountHeadController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(CommadResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateAccountHeadCommand request)
        {
            try
            {
                request.CreatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.Returnobj, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommadResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOfficeAssign([FromBody] List<HeadXOfficeAssignMapVM> request)
        {
            try
            {
                if (request.Any())
                {
                    string msg = request.Any(x => x.OfficeId == 0) ? "Office data missing" : request.Any(x => x.AccountId == 0) ? "Head missing" : "";
                    if (msg == "")
                    {
                        CreateAccountHeadXOfficeCommand obj = new CreateAccountHeadXOfficeCommand()
                        {
                            loggedinEmployeeId = loggedInEmployeeId,
                            lst = request
                        };
                        var response = await _mediator.Send(obj);
                        return CustomResult(response.Message, response.StatusCode);
                    }
                    else return CustomResult(msg, HttpStatusCode.BadRequest);
                }
                else
                    return CustomResult(MessageTexts.data_not_found, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(CommadResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateAccountHeadCommand request)
        {
            try
            {
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(CommadResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] DeleteAccountHeadCommand request)
        {
            try
            {
                request.DeletedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountHeadXChildVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccountHeads(int? parentId, string requestType = "L"/*L=page load request, E= expand*/ )
        {
            try
            {
                if (requestType == "L" || requestType == "E")
                {
                    var obj = await _mediator.Send(new GetAccountHeadQuery(parentId, requestType));
                    return CustomResult(obj);
                }
                else return CustomResult(new AccountHeadXChildVM { });

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(HeadXOfficeAssignVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfficeAssign(int accountId)
        {
            try
            {
                if (accountId > 0)
                {
                    var obj = await _mediator.Send(new GetOfficeAssignDetailQuery(loggedInOfficeId, accountId));
                    return CustomResult(obj);
                }
                else return CustomResult(new HeadXOfficeAssignVM { });
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
