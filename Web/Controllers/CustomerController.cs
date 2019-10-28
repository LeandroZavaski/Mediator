using System;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services.Interfaces;
using Barigui;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ApiModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomerService _customerService;

        public CustomerController(IMediator mediator, ICustomerService customerService)
        {
            _mediator = mediator;
            _customerService = customerService;
        }

        #region CRUD Dynamo
        // GET: api/Costumer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var pessoa = await _mediator.Send(new GetByIdCommand(id)).ConfigureAwait(false);

            if (pessoa == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Não foi possivel encontrar o registo!");
            }

            return Ok(pessoa);
        }

        // PUT: api/Costumer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Customer customer)
        {
            var result = await _mediator.Send(new GetByIdCommand(id)).ConfigureAwait(false);

            if (result == null)
            {
                await _mediator.Send(new CreateCommand(customer)).ConfigureAwait(false);
                return Ok();
            }

            await _mediator.Send(new UpdateCommand(customer)).ConfigureAwait(false);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pessoa = await _mediator.Send(new GetByIdCommand(id)).ConfigureAwait(false);

            if (pessoa == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Não foi possivel encontrar o registo!");
            }

            await _mediator.Send(new DeleteCommand(pessoa)).ConfigureAwait(false);

            return Ok();
        }
        #endregion

        #region Kinesis

        [HttpPut]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<ActionResult> PutCustomerKinesis(string id, [FromBody]CustomerInfoInsertionRequest customerInsertionRequest)
        {
            return await ProcessBaseEvent(id, customerInsertionRequest);
        }

        private async Task<ActionResult> ProcessBaseEvent(string id, CustomerBaseRequest request)
        {
            request.RequestId = Request.Headers.TryGetValue("Request-Id", out var requestId) ? requestId.ToString() : Guid.NewGuid().ToString();
            request.CustomerId = id;

            var baseEvent = (BaseEvent)request;

            await _customerService.ProcessBaseEvent(baseEvent);

            return Accepted();
        }

        #endregion
    }
}
