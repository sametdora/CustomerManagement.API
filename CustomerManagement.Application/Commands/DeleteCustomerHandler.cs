using MediatR;
using CustomerManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Commands
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.DeleteCustomerAsync(request.Id);
        }
    }
}
