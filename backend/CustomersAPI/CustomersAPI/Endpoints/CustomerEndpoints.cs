using CustomersAPI.Models;
using CustomersAPI.Repository;

namespace CustomersAPI.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapRoutes(WebApplication app)
        {
            app.MapGet("/customers", async (ICustomerRepository repository, [AsParameters] CustomerQueryParameters parameters) =>
            {
                var total = await repository.CountCustomers(parameters);
                var customers = repository.GetCustomers(parameters).ToList();

                return Results.Ok(new { total, customers });
            });

            app.MapPost("/customers", async (ICustomerRepository repository, Customer customer) =>
            {
                await repository.AddCustomer(customer);
                return Results.Created($"/customers/{customer.Id}", customer);
            });

            app.MapPut("/customers/{id}", async (ICustomerRepository repository, int id, Customer updatedCustomer) =>
            {
                var customer = await repository.UpdateCustomer(id, updatedCustomer);
                if (customer == null) return Results.NotFound();

                return Results.Ok(customer);
            });

            app.MapDelete("/customers/{id}", async (ICustomerRepository repository, int id) =>
            {
                var result = await repository.DeleteCustomer(id);
                if (!result) return Results.NotFound();

                return Results.NoContent();
            });
        }
    }
}
