using MediatR;

namespace CustomerManagement.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
