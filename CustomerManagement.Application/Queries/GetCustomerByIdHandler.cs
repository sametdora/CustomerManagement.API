using MediatR;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Queries
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerByIdAsync(request.Id);
        }
    }
}
