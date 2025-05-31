using CustomersAPI.DTOs;
using CustomersAPI.Models;
using CustomersAPI.Repository;

namespace CustomersAPI.Services
{
    public class CustomerService(ICustomerRepository repository) : ICustomerService
    {
        private readonly ICustomerRepository _repository = repository;

        public async Task<PagedCustomersResultDto> GetCustomers(CustomerQueryParameters parameters)
        {
            var total = await _repository.CountCustomers(parameters);
            var customers = _repository.GetCustomers(parameters).ToList();

            return new PagedCustomersResultDto { Customers = customers, TotalCount = total};
        }

        public async Task AddCustomer(Customer customer)
        {
            await _repository.AddCustomer(customer);
        }

        public async Task<Customer?> UpdateCustomer(int id, Customer updatedCustomer)
        {
            var customer = await _repository.UpdateCustomer(id, updatedCustomer);

            return customer;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var result = await _repository.DeleteCustomer(id);

            return result;
        }
    }
}
