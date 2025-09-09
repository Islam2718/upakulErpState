using System.Net;
using MediatR;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Queries.GroupCommittee;
using MF.Application.Features.DBOrders.Queries.Member;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers
{
    public class GroupCommitteeController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public GroupCommitteeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] List<GroupCommitteeRequestVM> request)
        {
            try
            {
                if (request.Any())
                {
                    CreateGroupCommitteeCommand obj = new CreateGroupCommitteeCommand
                    {
                        groupCommitteeRequests = request,
                        loggedInEmpId = loggedInEmployeeId
                    };
                    var response = await _mediator.Send(obj);
                    return CustomResult(response.Message, response.StatusCode);
                }
                else
                    return CustomResult("No data found to save", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
                
        [HttpGet]
        [ProducesResponseType(typeof(GroupCommitteeResponseVM), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGroupXCommitteeAllData(int groupId)
        {
            try
            {
                var lstObj = await _mediator.Send(new GetGroupXCommitteeAllDataQuery(groupId));
                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
