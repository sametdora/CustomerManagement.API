using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Threading.Tasks;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces;
using System.Data.Common;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private readonly SqlConnection _dbConnection;
        public CustomerRepository(SqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string customerJson = JsonConvert.SerializeObject(customer);

                var parameters = new { CustomerData = customerJson };

                return await dbConnection.ExecuteScalarAsync<int>("usp_CreateCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            string customerJson = await GetCustomerJsonByIdAsync(id);
            return string.IsNullOrEmpty(customerJson) ? null : JsonConvert.DeserializeObject<Customer>(customerJson);
        }

        public async Task<string> GetCustomerJsonByIdAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new { Id = id };
                return await dbConnection.QueryFirstOrDefaultAsync<string>("usp_GetCustomerById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string customerJson = JsonConvert.SerializeObject(customer);

                var parameters = new
                {
                    customer.Id,
                    CustomerData = customerJson
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
    }
}
