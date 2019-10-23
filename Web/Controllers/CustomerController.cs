using System.Threading.Tasks;
using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
    }
}
