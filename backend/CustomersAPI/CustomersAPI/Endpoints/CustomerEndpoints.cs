using CustomersAPI.DataContext;
using CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomersAPI.Endpoints
{
    public class CustomerQueryParameters
    {
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool Desc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public static class CustomerEndpoints
    {
        public static void MapRoutes(WebApplication app)
        {
            app.MapGet("/customers", async (AppDbContext db, [AsParameters] CustomerQueryParameters parameters) =>
            {
                var query = db.Customers.AsQueryable();

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

                var total = await query.CountAsync();
                var customers = await query.Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();

                return Results.Ok(new { total, customers });
            });

            app.MapPost("/customers", async (AppDbContext db, Customer customer) =>
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return Results.Created($"/customers/{customer.Id}", customer);
            });

            app.MapPut("/customers/{id}", async (AppDbContext db, int id, Customer updatedCustomer) =>
            {
                var customer = await db.Customers.FindAsync(id);
                if (customer == null) return Results.NotFound();

                customer.Name = updatedCustomer.Name;
                customer.Email = updatedCustomer.Email;
                await db.SaveChangesAsync();

                return Results.Ok(customer);
            });

            app.MapDelete("/customers/{id}", async (AppDbContext db, int id) =>
            {
                var customer = await db.Customers.FindAsync(id);
                if (customer == null) return Results.NotFound();

                db.Customers.Remove(customer);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
