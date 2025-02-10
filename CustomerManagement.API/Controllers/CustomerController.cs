using Microsoft.AspNetCore.Mvc;
using CustomerManagement.Application.Commands;
using CustomerManagement.Domain.Interfaces;
using MediatR;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;

        public CustomerController(ICustomerRepository customerRepository, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = result }, null);
            }

            return BadRequest("Customer could not be created.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var result = await _mediator.Send(command);
            if (result > 0)
            {
                return NoContent(); 
            }

            return BadRequest("Customer could not be updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Id = id });
            if (result > 0)
            {
                return NoContent(); 
            }

            return NotFound();
        }
    }
}
