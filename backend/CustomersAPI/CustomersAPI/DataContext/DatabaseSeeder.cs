using CustomersAPI.Models;

namespace CustomersAPI.DataContext
{
    public static class DatabaseSeeder
    {
        public static void Seed(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!db.Customers.Any()) 
            {
                db.Customers.AddRange(
                    new Customer { Name = "John Doe", Email = "john@example.com" },
                    new Customer { Name = "Jane Smith", Email = "jane@example.com" },
                    new Customer { Name = "Alice Brown", Email = "alice@example.com" },
                    new Customer { Name = "Bob Johnson", Email = "bob@example.com" },
                    new Customer { Name = "Emily Davis", Email = "emily@example.com" },
                    new Customer { Name = "Michael Wilson", Email = "michael@example.com" },
                    new Customer { Name = "Sarah Miller", Email = "sarah@example.com" },
                    new Customer { Name = "David Anderson", Email = "david@example.com" },
                    new Customer { Name = "Olivia Martinez", Email = "olivia@example.com" },
                    new Customer { Name = "James Taylor", Email = "james@example.com" },
                    new Customer { Name = "Sophia White", Email = "sophia@example.com" },
                    new Customer { Name = "Daniel Harris", Email = "daniel@example.com" },
                    new Customer { Name = "Isabella Clark", Email = "isabella@example.com" },
                    new Customer { Name = "Matthew Lewis", Email = "matthew@example.com" },
                    new Customer { Name = "Ava Walker", Email = "ava@example.com" }
                );
                db.SaveChanges();
            }
        }
    }
}
