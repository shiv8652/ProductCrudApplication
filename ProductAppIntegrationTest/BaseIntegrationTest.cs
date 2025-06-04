using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCrudApplication.Data;
using ProductCrudApplication.Model;

namespace ProductAppIntegrationTest
{
    public class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        protected readonly WebApplicationFactory<Program> _factory;

        protected BaseIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = CreateTestClient();
        }


        private HttpClient CreateTestClient()
        {
            var appFactory = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove existing DbContext
                    var descriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                        //services.SaveChanges();
                    }

                    // Add in-memory database with unique name      
                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestProdDb");
                        //options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid().ToString()}");
                    });

                    // Seed common test data
                    SeedTestData(services);
                });
            });

            return appFactory.CreateClient();
        }

        protected virtual void SeedTestData(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //var bools = db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            // Add common test data here
            SeedCategories(db);
            SeedProducts(db);

            db.SaveChanges();
        }

        protected virtual void SeedCategories(AppDbContext db)
        {
            db.Categories.AddRange(
                new Category {  Name = "Electronics", Description = "Electronic items" },
                new Category {  Name = "Books", Description = "Educational books" },
                new Category {  Name = "Automobile", Description = "automobile items" }
            );
        }

        protected virtual void SeedProducts(AppDbContext db)
        {
            db.Products.AddRange(
                new Product { Name = "Laptop", Price = 999.99m, CategoryId = 1 },
                new Product { Name = "Phone", Price = 600.00m, CategoryId = 1 },
                new Product { Name = "Jack & Jones", Price = 599.99m, CategoryId = 1 }
            );
        }

    }
}
