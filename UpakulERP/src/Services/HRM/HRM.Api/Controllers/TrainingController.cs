using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utility.CommonController;

namespace HRM.Api.Controllers
{
    public class TrainingController : ApiController
    {
        #region Var
        IMediator _mediator;
        #endregion Var
        public TrainingController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
