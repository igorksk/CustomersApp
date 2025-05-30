using CustomersAPI.Endpoints;
using CustomersAPI.Models;

namespace CustomersAPI.Repository
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetCustomers(CustomerQueryParameters parameters);

        Task<int> CountCustomers(CustomerQueryParameters parameters);

        Task AddCustomer(Customer customer);

        Task<Customer?> UpdateCustomer(int id, Customer updatedCustomer);

        Task<bool> DeleteCustomer(int id);
    }
}
