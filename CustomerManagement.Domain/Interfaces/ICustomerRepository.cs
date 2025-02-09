using CustomerManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<int> CreateCustomerAsync(Customer customer);
        Task<int> UpdateCustomerAsync(Customer customer);
        Task<int> DeleteCustomerAsync(int id);
    }
}
