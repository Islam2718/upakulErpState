using System.Net;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.GroupCommittee;
using MF.Application.Features.DBOrders.Queries.Member;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers
{
    public class MemberController : ApiController
    {
        #region Var
        IMediator _mediator;
        IMemberRepository _repository;
        #endregion Var
        public MemberController(IMediator mediator, IMemberRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Checker([FromForm] CreateMemberCommand request)
        {
            try
            {
                var varifyMsg = _repository.MemberDataCheck(request.NationalId, request.SmartCard, request.ContactNoOwn);
                if (varifyMsg != "")
                    return CustomResult(varifyMsg, HttpStatusCode.NotAcceptable);
                else return Ok();
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromForm] CreateMemberCommand request)
        {
            try
            {
                request.OfficeId = loggedInOfficeId.Value;
                request.CreatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

       

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromForm] UpdateMemberCommand request)
        {
            try
            {
                //request.OfficeId = loggedInOfficeId.Value;
                request.UpdatedBy = loggedInEmployeeId;
                var response = await _mediator.Send(request);
                return CustomResult(response.Message, response.StatusCode);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> MigrateMember([FromBody] UpdateMemberMigrateCommand request)
        {
            try
            {
                if (request != null)
                {
                    if (request.MemberId == 0 || string.IsNullOrEmpty(request.MigratedNote))
                        return CustomResult(request.MemberId == 0 ? "Member information not found" : "Note not found", HttpStatusCode.BadRequest);
                    else
                    {
                        request.MigrateBy = loggedInEmployeeId; // logged in employee
                        var response = await _mediator.Send(request);
                        return CustomResult(response.Message, response.StatusCode);
                    }
                }
                else return CustomResult("Data not found", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Approved([FromBody] UpdateMemberStatusCommand request)
        {
            if (request.IsApproved) request.ApprovedBy = loggedInEmployeeId;
            else request.RejectBy = loggedInEmployeeId;

            var response = await _mediator.Send(request);
            return CustomResult(response.Message, response.StatusCode);
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> MobileNoVerified([FromBody] UpdateMemberMobileNoCheckedCommand request)
        {
            if (request != null)
            {
                if(request.MemberId== 0 || string.IsNullOrEmpty(request.OTPNo))
                    return CustomResult(request.MemberId == 0?"Member information not found":"Otp not found", HttpStatusCode.BadRequest);
                else
                {
                    request.CheckedBy = loggedInEmployeeId;
                    request.IsChecked = true;
                    request.CheckedDate = DateTime.Now;
                    var response = await _mediator.Send(request);
                    return CustomResult(response.Message, response.StatusCode);
                }
            }
            else return CustomResult("Data not found", HttpStatusCode.BadRequest);

            
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] UpdateMemberApprovedCommand request)
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
        public async Task<IActionResult> LoadGrid(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 0,
        [FromQuery] string search = "",
        [FromQuery] string SortOrder = "")
        {
            var query = new MemberGridQuery
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                SortOrder = SortOrder,
                logedInOfficeId = loggedInOfficeId.Value
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(MemberVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _mediator.Send(new MemberByIdQuery(id));
            return CustomResult(obj);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MemberVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMemberDetailById(int id)
        {
            var obj = await _mediator.Send(new MemberDetailByIdQuery(id));
            return CustomResult(obj);
        }

        #region Drop down
        [HttpGet]
        [ProducesResponseType(typeof(MultipleDropdownForMemberProfileVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllDropDownData()
        {
            var obj = await _mediator.Send(new MemberProfileDropdownQuery(loggedInOfficeId ?? 0));
            return CustomResult(obj);
        }           

        [HttpGet]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMemberDropdown(int? groupId)
        {
            try
            {
                var lstObj = await _mediator.Send(new MemberDropdownQuery(officeId: loggedInOfficeId??0, groupId: groupId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}

