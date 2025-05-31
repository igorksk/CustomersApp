using CustomersAPI.DTOs;
using CustomersAPI.Models;

namespace CustomersAPI.Services
{
    public interface ICustomerService
    {
        Task<PagedCustomersResultDto> GetCustomers(CustomerQueryParameters parameters);

        Task AddCustomer(Customer customer);

        Task<Customer?> UpdateCustomer(int id, Customer updatedCustomer);

        Task<bool> DeleteCustomer(int id);
    }
}
