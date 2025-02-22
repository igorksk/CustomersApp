using CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.DataContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
