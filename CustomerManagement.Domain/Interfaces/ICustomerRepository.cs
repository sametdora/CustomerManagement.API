using System.Threading.Tasks;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<int> UpdateCustomerAsync(Customer customer);
        Task<int> DeleteCustomerAsync(int id);
    }
}
