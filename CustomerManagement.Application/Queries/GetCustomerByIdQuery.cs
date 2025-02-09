using CustomerManagement.Domain.Entities;
using MediatR;

namespace CustomerManagement.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
