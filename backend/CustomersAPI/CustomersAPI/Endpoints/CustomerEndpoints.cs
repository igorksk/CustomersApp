using CustomersAPI.Models;
using CustomersAPI.Services;

namespace CustomersAPI.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapRoutes(WebApplication app)
        {
            app.MapGet("/customers", async (ICustomerService service, [AsParameters] CustomerQueryParameters parameters) =>
            {
                var result = await service.GetCustomers(parameters);

                return Results.Ok(new { total = result.TotalCount, result.Customers });
            });

            app.MapPost("/customers", async (ICustomerService service, Customer customer) =>
            {
                await service.AddCustomer(customer);
                return Results.Created($"/customers/{customer.Id}", customer);
            });

            app.MapPut("/customers/{id}", async (ICustomerService service, int id, Customer updatedCustomer) =>
            {
                var customer = await service.UpdateCustomer(id, updatedCustomer);
                if (customer == null) return Results.NotFound();

                return Results.Ok(customer);
            });

            app.MapDelete("/customers/{id}", async (ICustomerService service, int id) =>
            {
                var result = await service.DeleteCustomer(id);
                if (!result) return Results.NotFound();

                return Results.NoContent();
            });
        }
    }
}
