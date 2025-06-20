using CustomersAPI.DataContext;
using CustomersAPI.Endpoints;
using CustomersAPI.Repository;
using CustomersAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomersAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("CustomersDb"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            var app = builder.Build();

            // Ensure database is created (for in-memory database)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            DatabaseSeeder.Seed(app);
            CustomerEndpoints.MapRoutes(app);

            app.Run();
        }
    }
}
