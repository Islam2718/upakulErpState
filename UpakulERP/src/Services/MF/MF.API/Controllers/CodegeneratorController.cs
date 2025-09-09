using MediatR;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Queries.CommonIdGenerator;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility.CommonController;
using Utility.Domain;

namespace MF.API.Controllers
{
    public class CodeGeneratorController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public CodeGeneratorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(CustomSelectListItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCodegenerators(int? id)
        {
            try
            {
                var lstObj = await _mediator.Send(new IdGenerateQuery());

                return CustomResult(lstObj);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] List<UpdateCodeGeneratorCommand> request)
        {
            // Wrap the list in a new command
            var response = await _mediator.Send(new UpdateCodeGeneratorListCommand { Items = request });

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var updatedList = await _mediator.Send(new IdGenerateQuery());
                return Ok(updatedList);
            }

            return StatusCode((int)response.StatusCode, new { message = response.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            var result = await _mediator.Send(new IdGenerateQuery());
            return Ok(result);
        }
    }
}


