using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Contacts.Enums;
using Project.Application.Features.DBOrders.Commands.Create.Commands;
using Project.Application.Features.DBOrders.Commands.Update.Commands;
using Project.Application.Features.DBOrders.Queries.ActivityPlan;
using System.Net;
using Utility.CommonController;
using Utility.Constants;
using Utility.Domain;

namespace Project.API.Controllers
{
    public class ActivityPlanController : ApiController
    {
        IMediator _mediator;
        public ActivityPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateActivityPlanCommand request)
        {
            try
            {
                if(request.ProjectId==0)
                    return CustomResult("Project is required", HttpStatusCode.BadRequest);
                else if(request.lst==null)
                    return CustomResult(MessageTexts.data_not_found, HttpStatusCode.BadRequest);
                else if (request.lst.Any())
                    return CustomResult(MessageTexts.data_not_found, HttpStatusCode.BadRequest);
                else if(request.lst.Any(x=>DateTime.MinValue.Equals(x.ReportingDate)) 
                    || request.lst.Any(x => DateTime.MinValue.Equals(x.ActivityFrom))
                    || request.lst.Any(x => DateTime.MinValue.Equals(x.ActivityTo)))
                    return CustomResult("Activity date are required", HttpStatusCode.BadRequest);
                else
                {
                    request.loggedinEmpId = loggedInEmployeeId ?? 0;
                    var response = await _mediator.Send(request);
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
        public async Task<IActionResult> Approved([FromBody] UpdateActivityPlanApprovedCommand request)
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjectXActivity(int projectId)
        {

            var obj = await _mediator.Send(new ActivityPlanListQuery(projectId));
            return CustomResult(obj);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadTargetType()
        {
            var lst = new TargetType().GetTargetTypeDropDown();
            return CustomResult(lst);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomSelectListItem>), (int)HttpStatusCode.OK)]
        public IActionResult LoadActivityStatus()
        {
            var lst = new ActivityStatus().GetActivityStatusDropDown();
            return CustomResult(lst);
        }
    }
}
