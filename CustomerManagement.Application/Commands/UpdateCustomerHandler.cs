using MediatR;
using CustomerManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Application.Commands
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            return await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
