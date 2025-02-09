using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    customer.Name,
                    customer.Email,
                    customer.Phone
                };
                return await dbConnection.ExecuteScalarAsync<int>("usp_CreateCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    customer.Id,
                    customer.Name,
                    customer.Email,
                    customer.Phone
                };
                return await dbConnection.ExecuteAsync("usp_UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> DeleteCustomerAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new { Id = id };
                return await dbConnection.ExecuteAsync("usp_DeleteCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new { Id = id };
                return await dbConnection.QueryFirstOrDefaultAsync<Customer>("usp_GetCustomerById", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
