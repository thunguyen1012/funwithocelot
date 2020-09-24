using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Data;
using System;

namespace Order.WebAPI
{
    public static class SeedData
    {
        private static readonly Core.Entities.Order order1 = new Core.Entities.Order
        {
            Id = 1,
            ProductId = 10,
            PaymentId = 20,
            Created = DateTime.UtcNow,
            CreatedBy = "admin",
            Status = Core.Entities.OrderStatus.New
        };

        private static readonly Core.Entities.Order order2 = new Core.Entities.Order
        {
            Id = 2,
            ProductId = 11,
            PaymentId = 21,
            Created = DateTime.UtcNow,
            CreatedBy = "admin",
            Status = Core.Entities.OrderStatus.New
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
            PopulateTestData(dbContext);
        }

        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.Orders)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            dbContext.Orders.Add(order1);
            dbContext.Orders.Add(order2);

            dbContext.SaveChanges();
        }
    }
}