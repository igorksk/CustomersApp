using CustomersAPI.Models;

namespace CustomersAPI.DTOs
{
    public class PagedCustomersResultDto
    {
        public IEnumerable<Customer> Customers { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
