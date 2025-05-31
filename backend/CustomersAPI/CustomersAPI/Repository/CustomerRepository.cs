using CustomersAPI.DataContext;
using CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomersAPI.Repository
{
    public class CustomerRepository(AppDbContext context) : ICustomerRepository
    {
        private readonly AppDbContext _context = context;

        public IQueryable<Customer> GetCustomers(CustomerQueryParameters parameters)
        {
            var query = FilterAndSortCustomers(parameters);

            var customers = query.Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize);

            return customers;
        }

        public async Task<int> CountCustomers(CustomerQueryParameters parameters)
        {
            var query = FilterAndSortCustomers(parameters);

            var total = await query.CountAsync();

            return total;
        }

        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> UpdateCustomer(int id, Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        private IQueryable<Customer> FilterAndSortCustomers(CustomerQueryParameters parameters)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Search))
            {
                query = query.Where(c => c.Name.Contains(parameters.Search) || c.Email.Contains(parameters.Search));
            }

            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                var property = typeof(Customer).GetProperty(parameters.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    query = parameters.Desc
                        ? query.OrderByDescending(c => EF.Property<object>(c, property.Name))
                        : query.OrderBy(c => EF.Property<object>(c, property.Name));
                }
            }

            return query;
        }
    }
}
