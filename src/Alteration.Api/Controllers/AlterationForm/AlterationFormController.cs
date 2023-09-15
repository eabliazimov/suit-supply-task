using System;
using System.Threading;
using System.Threading.Tasks;
using Alteration.Application.Features.AlterationCompleted;
using Alteration.Application.Features.AlterationStarted;
using Alteration.Application.Features.GetAlterationFormsQuery;
using Alteration.Application.Features.PaymentReceived;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alteration.Api.Controllers.AlterationForm
{
    [ApiController]
    [Route("api/alteration-forms")]
    public class AlterationFormController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlterationFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAlterationForm(
            CreateAlterationFormModel alterationFormModel, 
            CancellationToken cancellationToken)
        {
            await _mediator.Send(alterationFormModel.ToCreateAlterationFormCommand(), cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAlterationForms()
        {
            var result = await _mediator.Send(new GetAlterationFormsQuery());

            return Ok(result);
        }

        [HttpPost]
        [Route("{id}/payment-received")]
        public async Task<IActionResult> PaymentReceived(Guid id)
        {
            await _mediator.Send(new PaymentReceivedCommand(id));

            return Ok();
        }

        [HttpPost]
        [Route("{id}/alteration-started")]
        public async Task<IActionResult> PostAlterationStarted(Guid id)
        {
            await _mediator.Send(new AlterationStartedCommand(id));

            return Ok();
        }

        [HttpPost]
        [Route("{id}/alteration-completed")]
        public async Task<IActionResult> PostAlterationCompleted(Guid id)
        {
            await _mediator.Send(new AlterationCompletedCommand(id));

            return Ok();
        }
    }
}
