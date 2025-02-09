using CustomerManagement.Application.Commands;
using CustomerManagement.Application.Queries;
using CustomerManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagement.API.Controllers
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = customerId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Id = id });
            if (result == 0) return NotFound();
            return NoContent();
        }

    }
}
