﻿using MediatR;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Commands
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            return await _customerRepository.CreateCustomerAsync(customer);
        }
    }
}
